using LeerTedXML.CSVCreate;
using LeerTedXML.XMLDocument;
using LeerTedXML.XMLReader;
using System.Diagnostics;
using System.Reflection;
using TedXMLExport.Menu;
using TedXMLExport.XMLTedObject;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Read Tenders Online Daily";
            Console.CursorVisible = true;

            RunMainMenu();

        }


        //methods
        public static void RunMainMenu()
        {
            string separator     = "****************************************************";
            string prompt        = "******** READ Tenders Online Daily XML files *******";
            string[] options = {"Read .xml file",
                                "Read .xml files in folder",
                                "Credits (WIP)",
                                "Exit" };

            Menu mainMenu = new Menu(prompt, options, separator);
            int selectedIndex = mainMenu.ChooseOptions();

            int readOption; int fileOption;

            switch (selectedIndex)
            {
                case 0:
                    readOption = 2;
                    fileOption = RunExportMenu();
                    DispatchOptions(fileOption, readOption);
                    RunMainMenu();
                    break;
                case 1:
                    readOption = 1;
                    fileOption = RunExportMenu();
                    DispatchOptions(fileOption, readOption);
                    RunMainMenu();
                    break;
                case 2:
                    ShowCredits();
                    break;
                case 3:
                    ConsoleUtils.ExitMenu();
                    break;
            }

        }

        public static int RunExportMenu()
        {
            string separator    = "****************************************************";
            string prompt       = "****************** EXPORT options ******************";
            string[] txtoptions = { "Append to former .txt file",
                                    "Create new .txt file",
                                    "Only print on console",
                                    "Go back" };

            Menu csvMenu = new Menu(prompt, txtoptions, separator);
            int selectedIndex = csvMenu.ChooseOptions();

            int txt_options = 0;
            switch (selectedIndex)
            {
                case 0:
                    txt_options += 1;
                    break;
                case 1:
                    txt_options += 2;
                    break;
                case 2:
                    txt_options += 3;
                    break;
                case 3:
                    Console.Clear();
                    RunMainMenu();
                    break;
            }

            return txt_options;

        }

        static void DispatchOptions(int fileOption, int readOption)
        {
            XMLRead xmlRead = new XMLRead();
            XMLDoc xmlDoc = new XMLDoc(xmlRead);
            TXTCreate csvCreate = new TXTCreate(xmlDoc);

            if (fileOption == 1)
            {
                Stopwatch watch = new Stopwatch();
                ConsoleUtils.StartTimer(watch);

                //"Append to former .csv file"
                csvCreate.AppendTxt(readOption);

                ConsoleUtils.StopTimer(watch);
                ConsoleUtils.BackToMenu();
            }
            else if (fileOption == 2)
            {
                Stopwatch watch = new Stopwatch();
                ConsoleUtils.StartTimer(watch);

                //"Create new .csv file" and read xml
                csvCreate.CreateNewTxt(readOption);

                ConsoleUtils.StopTimer(watch);
                ConsoleUtils.BackToMenu();
            }
            else if (fileOption == 3)
            {
                Stopwatch watch = new Stopwatch();
                ConsoleUtils.StartTimer(watch);

                //print on console, contolText
                csvCreate.ReadXml(readOption);

                ConsoleUtils.StopTimer(watch);
                ConsoleUtils.BackToMenu();
            }
        }


        private static void ShowCredits()
        {
            ConsoleUtils.WriteLineMenuColor("\t> Name of project");
            Console.WriteLine("\t" + Assembly.GetExecutingAssembly().GetName().Name);

            ConsoleUtils.WriteLineMenuColor("\t> Description:");
            Console.WriteLine("\tConsole applictation to filter and parse TED documents." +
                "\n\tFiltering conditions:" +
                "\n\t - CPV starting w/ '71' -archiecture and engineering services- " +
                "\n\t - F02 or F12 forms -contract notices and design contest notices-.");

            ConsoleUtils.WriteLineMenuColor("\t> Version");
            Console.WriteLine("\t" + Assembly.GetExecutingAssembly().GetName().Version);

            ConsoleUtils.WriteLineMenuColor("\t> Target runtime environment");
            Console.WriteLine("\tPortable");

            string publish_folder = @"" +
                "G:\\Unidades compartidas\\P10380 I+D\\P10380 I+D Tenders Online Daily\\dev\\TendersOnlineDaily\\TedXMLExport\\bin\\Publish\\TedXMLExport.exe";
            string exe_last_modified = System.IO.File.GetLastWriteTime(publish_folder).ToShortDateString();
            ConsoleUtils.WriteLineMenuColor("\t> Project last published on:");
            Console.WriteLine("\t" + exe_last_modified);

            string project_last_modified = System.IO.File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToShortDateString();
            ConsoleUtils.WriteLineMenuColor("\t> Project last edited on:");
            Console.WriteLine("\t" + project_last_modified);

            ConsoleUtils.WriteLineMenuColor("\t> Authorship");
            Console.WriteLine("\tApplied R&D team. MorphEstudio. ©2023");

            ConsoleUtils.BackToMenu();
        }

        //TODO Move to another class
        public static void PrintControlText(List<TED_EXPORT> ted_exportList)
        {
            ConsoleUtils.WriteLineInfoColor("INFO Console print:");
            //WIP just to check
            foreach (TED_EXPORT ted in ted_exportList)
            {
                ConsoleUtils.WriteLineInfoColor($"INFO Current object: {ted.DOC_ID}");

                //ID and dates
                ConsoleUtils.WriteLineEventColor($"ted object ID: {ted.DOC_ID}");
                ConsoleUtils.WriteLineEventColor($"coded_data_section.notice_data.NO_DOC_OJS = {ted.CODED_DATA_SECTION!._NOTICE_DATA!._NO_DOC_OJS!._Text}");
                ConsoleUtils.WriteLineEventColor($"coded_data_section.ref_ojs.DATE_PUB= {ted.CODED_DATA_SECTION!._REF_OJS!._DATE_PUB!._Text}");
                ConsoleUtils.WriteLineEventColor($"technical_section.DELETION_DATE = {ted.TECHNICAL_SECTION!._DELETION_DATE!._Text}");

                //ted uri
                string uri_list = "coded_data_section.notice_data.uri_list.URI_DOC = ";
                int uri_count = ted.CODED_DATA_SECTION!._NOTICE_DATA!._URI_LIST!._URI_DOC!.Count();
                for (int i = 0; i < uri_count; i++)
                {
                    var element = ted.CODED_DATA_SECTION!._NOTICE_DATA!._URI_LIST!._URI_DOC![i];
                    if (i == (uri_count - 1))
                    {
                        uri_list += $"{element._Text}";
                    }
                    else
                    {
                        uri_list += $"{element._Text} //";
                    }

                }
                ConsoleUtils.WriteLineEventColor(uri_list);

                //LANGUAAGE AND FORMS INFO
                ConsoleUtils.WriteLineEventColor($"technical_section.notice_data.LG_ORIG = {ted.CODED_DATA_SECTION!._NOTICE_DATA!._LG_ORIG!._Text}");
                ConsoleUtils.WriteLineEventColor($"form_section.form.CAT/FORM = " +
                    $"{ted.FORM_SECTION!._FORM!._CATEGORY} // {ted.FORM_SECTION!._FORM!._FORM}");
                ConsoleUtils.WriteLineEventColor($"form_section.form.DIRECTIVE = {ted.FORM_SECTION!._FORM!._DIRECTIVE!._VALUE}");

                //LOCALIZATION
                int ml_ti_doc_count = ted.TRANSLATION_SECTION!._ML_TITLES!._ML_TI_DOC!.Count();
                string ti_cy = "translation_section.ml_titles_ml_ti_doc_TI_CY = ";
                string ti_towm = "translation_section.ml_titles_ml_ti_doc_TI_TOWN = ";
                string ti_text = "translation_section.ml_titles_ml_ti_doc_TI_TEXT= ";
                for(int i=0; i<ml_ti_doc_count; i++)
                {
                    var element = ted.TRANSLATION_SECTION!._ML_TITLES!._ML_TI_DOC![i];
                    if (i == (ml_ti_doc_count - 1))
                    {
                        ti_cy+= $"{element._TI_CY!._Text}";
                        ti_towm += $"{element._TI_TOWN!._Text}";

                        int ml_ti_text_p_count = element._TI_TEXT!._P!.Count();
                        for(int j = 0; j< ml_ti_text_p_count; j++)
                        {
                            var el = ted.TRANSLATION_SECTION!._ML_TITLES!._ML_TI_DOC![i]._TI_TEXT!._P![j];
                            ti_text += $"{el._Text}";
                        }
                    }
                }
                ConsoleUtils.WriteLineEventColor(ti_cy);
                ConsoleUtils.WriteLineEventColor(ti_towm);
                ConsoleUtils.WriteLineEventColor(ti_text);

                //CPV
                string original_cpv_list = "coded_data_section.notice_data.ORIGINAL_CPV = ";
                int original_cpv_count = ted.CODED_DATA_SECTION!._NOTICE_DATA!._ORIGINAL_CPV!.Count();
                for (int i = 0; i < original_cpv_count; i++)
                {
                    var element = ted.CODED_DATA_SECTION!._NOTICE_DATA!._ORIGINAL_CPV![i];
                    if (i == (original_cpv_count - 1))
                    {
                        original_cpv_list += $"{element._CODE} - {element._Text}";
                    }
                    else
                    {
                        original_cpv_list += $"{element._CODE} - {element._Text} //";
                    }
                }
                ConsoleUtils.WriteLineEventColor(original_cpv_list);

                //CODIF_DATA
                ConsoleUtils.WriteLineEventColor($"technical_section.codif_data._AA_AUTHORITY_TYPE = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._AA_AUTHORITY_TYPE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._AA_AUTHORITY_TYPE!._Text}");
                ConsoleUtils.WriteLineEventColor($"technical_section.codif_data._TD_DOCUMENT_TYP = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._TD_DOCUMENT_TYPE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._TD_DOCUMENT_TYPE!._Text}");
                ConsoleUtils.WriteLineEventColor($"technical_section.codif_data._NC_CONTRACT_NATURE = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._NC_CONTRACT_NATURE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._NC_CONTRACT_NATURE!._Text}");
                ConsoleUtils.WriteLineEventColor($"technical_section.codif_data._PR_PROC = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._PR_PROC!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._PR_PROC!._Text}");
                ConsoleUtils.WriteLineEventColor($"technical_section.codif_data._RP_REGULATION = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._RP_REGULATION!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._RP_REGULATION!._Text}");
                ConsoleUtils.WriteLineEventColor($"technical_section.codif_data._TY_TYPE_BID = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._TY_TYPE_BID!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._TY_TYPE_BID!._Text}");
                ConsoleUtils.WriteLineEventColor($"technical_section.codif_data._AC_AWARD_CRIT = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._AC_AWARD_CRIT!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._AC_AWARD_CRIT!._Text}");
                ConsoleUtils.WriteLineEventColor($"technical_section.codif_data._MA_MAIN_ACTIVITIES = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._MA_MAIN_ACTIVITIES!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._MA_MAIN_ACTIVITIES!._Text}");

                //OBJECT_CONTRACT
                string ob_title_list = "form_section.form.object_contract.TITLE = ";
                int ob_title_count = ted.FORM_SECTION!._FORM!._OBJECT_CONTRACT!._TITLE!._P!.Count();
                for (int i = 0; i < ob_title_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM!._OBJECT_CONTRACT!._TITLE!._P![i];
                    if(i == 0)
                    {
                        ob_title_list += $"{element._Text.ToUpper()}";
                    }
                    else if (i == (ob_title_count - 1))
                    {
                        ob_title_list += $"\n{element._Text.ToUpper()}";
                    }
                    else
                    {
                        ob_title_list += $"\n{element._Text.ToUpper()}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(ob_title_list);
                
                string ob_shortdescr_list = "form_section.form.object_contract.SHORT_DESCR = ";
                int ob_shortdescr_count = ted.FORM_SECTION!._FORM._OBJECT_CONTRACT!._OBJECT_DESCR!._SHORT_DESCR!._P!.Count();
                for (int i = 0; i < ob_shortdescr_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._OBJECT_CONTRACT!._OBJECT_DESCR!._SHORT_DESCR!._P![i];
                    if( i == 0)
                    {
                        ob_shortdescr_list += $"{element._Text}";
                    }
                    else if(i == (ob_shortdescr_count - 1))
                    {
                        ob_shortdescr_list += $"\n{element._Text}";
                    }
                    else
                    {
                        ob_shortdescr_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(ob_shortdescr_list);
                
                //CONTRACTING_BODY
                ConsoleUtils.WriteLineEventColor($"form_section.form.contracting_body.CA_TYPE = " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_TYPE!._VALUE}");
                ConsoleUtils.WriteLineEventColor($"form_section.form.contracting_body.CA_TYPE_OTHER = " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_TYPE_OTHER!._Text}");
                ConsoleUtils.WriteLineEventColor($"form_section.form.contracting_body.CA_ACTIVITY = " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_ACTIVITY!._VALUE}");
                ConsoleUtils.WriteLineEventColor($"form_section.form.contracting_body.URL_DOCUMENT = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._URL_DOCUMENT!._Text}");
                ConsoleUtils.WriteLineEventColor($"form_section.form.contracting_body.URL_PARTICIPATION = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._URL_PARTICIPATION!._Text}");

                //PROCEDURE
                ConsoleUtils.WriteLineEventColor($"form_section.form.procedure.PROCEDURE_TYPE = " +
                   $"{ted.FORM_SECTION!._FORM._PROCEDURE!._first_element!.ToUpper()}");

                if(ted.FORM_SECTION!._FORM._PROCEDURE!._NB_PARTICIPANTS!._Text is not null)
                {
                    ConsoleUtils.WriteLineEventColor($"form_section.form.procedure.PARTICIPANTS = " +
                   $"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_PARTICIPANTS!._Text}");
                }
                else
                {
                    ConsoleUtils.WriteLineEventColor($"form_section.form.procedure.PARTICIPANTS = " +
                       $"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_MIN_PARTICIPANTS!._Text} - {ted.FORM_SECTION!._FORM._PROCEDURE!._NB_MAX_PARTICIPANTS!._Text}");
                }

                ConsoleUtils.WriteLineEventColor($"form_section.form.procedure.DATE_RECEIPT_DATE = " +
                  $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DATE_RECEIPT_TENDERS!._Text} / {ted.FORM_SECTION!._FORM._PROCEDURE!._TIME_RECEIPT_TENDERS!._Text}");
                
                string pr_lang_list = "form_section.form.procedure.LANG = ";
                int pr_lang_count = ted.FORM_SECTION!._FORM._PROCEDURE!._LANGUAGES!._LANGUAGE!.Count();
                for (int i = 0; i < pr_lang_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._PROCEDURE!._LANGUAGES._LANGUAGE![i];
                    if (i == 0)
                    {
                        pr_lang_list += $"{element._VALUE}";
                    }
                    else if (i == (pr_lang_count - 1))
                    {
                        pr_lang_list += $"\n{element._VALUE}";
                    }
                    else
                    {
                        pr_lang_list += $"\n{element._VALUE}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(pr_lang_list);

                string num_val_price_list = "form_section.form.procedure.NUMBER_VALUE_PRICE = ";
                int num_val_price_count = ted.FORM_SECTION!._FORM._PROCEDURE!._NUMBER_VALUE_PRIZE!._P!.Count();
                for (int i = 0; i < num_val_price_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._PROCEDURE!._NUMBER_VALUE_PRIZE._P![i];
                    if (i == 0)
                    {
                        num_val_price_list += $"{element._Text}";
                    }
                    else if (i == (num_val_price_count - 1))
                    {
                        num_val_price_list += $"\n{element._Text}";
                    }
                    else
                    {
                        num_val_price_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(num_val_price_list);

                string criteria_ev_list = "form_section.form.procedure.CRITERIA_EVAL = ";
                int criteria_ev_count = ted.FORM_SECTION!._FORM._PROCEDURE!._CRITERIA_EVALUATION!._P!.Count();
                for (int i = 0; i < criteria_ev_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._PROCEDURE!._CRITERIA_EVALUATION!._P![i];
                    if (i == 0)
                    {
                        criteria_ev_list += $"{element._Text}";
                    }
                    else if (i == (criteria_ev_count - 1))
                    {
                        criteria_ev_list += $"\n{element._Text}";
                    }
                    else
                    {
                        criteria_ev_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(criteria_ev_list);

                if(ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._TYPE != String.Empty)
                {
                ConsoleUtils.WriteLineEventColor($"form_section.form.procedure.DURATION_TENDER = " +
                   $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._Text} / {ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._TYPE}");
                }
                else
                { 
                    ConsoleUtils.WriteLineEventColor($"form_section.form.procedure.DURATION_TENDER = " +
                   $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._Text}");
                }

                //LEFTI
                string criteria_sel_list = "form_section.form.lefti.CRITERIA_SELECTION = ";
                int criteria_sel_count = ted.FORM_SECTION!._FORM._LEFTI!._CRITERIA_SELECTION!._P!.Count();
                for (int i = 0; i < criteria_sel_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._PROCEDURE!._CRITERIA_EVALUATION!._P![i];
                    if (i == 0)
                    {
                        criteria_sel_list += $"{element._Text}";
                    }
                    else if (i == (criteria_sel_count - 1))
                    {
                        criteria_sel_list += $"\n{element._Text}";
                    }
                    else
                    {
                        criteria_sel_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(criteria_sel_list);

                string suitability_list = "form_section.form.lefti.SUITABILITY = ";
                int suitability_count = ted.FORM_SECTION!._FORM._LEFTI!._SUITABILITY!._P!.Count();
                for (int i = 0; i < suitability_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._LEFTI!._SUITABILITY!._P![i];
                    if (i == 0)
                    {
                        suitability_list += $"{element._Text}";
                    }
                    else if (i == (suitability_count - 1))
                    {
                        suitability_list += $"\n{element._Text}";
                    }
                    else
                    {
                        suitability_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(suitability_list);

                string particular_prof_list = "form_section.form.lefti.PARTICULAR_PROF = ";
                int particular_prof_count = ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._P!.Count();
                for (int i = 0; i < particular_prof_count; i++)
                {
                    //FALTA EL CTYPE
                    var element = ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._P![i];
                    if(ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._CTYPE! != String.Empty)
                    {
                        particular_prof_list += $"CTYPE: {ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._CTYPE!}/ ";
                    }
                    
                    if (i == 0)
                    {
                        particular_prof_list += $"{element._Text}";
                    }
                    else if (i == (particular_prof_count - 1))
                    {
                        particular_prof_list += $"\n{element._Text}";
                    }
                    else
                    {
                        particular_prof_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(particular_prof_list);

                string economic_fin_info_list = "form_section.form.lefti.ECONOMIC_FINANCIAL_INFO = ";
                int economic_fin_info_count = ted.FORM_SECTION!._FORM._LEFTI!._ECONOMIC_FINANCIAL_INFO!._P!.Count();
                for (int i = 0; i < economic_fin_info_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._LEFTI!._ECONOMIC_FINANCIAL_INFO!._P![i];
                    if (i == 0)
                    {
                        economic_fin_info_list += $"{element._Text}";
                    }
                    else if (i == (economic_fin_info_count - 1))
                    {
                        economic_fin_info_list += $"\n{element._Text}";
                    }
                    else
                    {
                        economic_fin_info_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(economic_fin_info_list);

                string tech_prof_list = "form_section.form.lefti.TECHNICAL_PROFESIONAL_INFO = ";
                int tech_prof_count = ted.FORM_SECTION!._FORM._LEFTI!._TECHNICAL_PROFESSIONAL_INFO!._P!.Count();
                for (int i = 0; i < tech_prof_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._LEFTI!._TECHNICAL_PROFESSIONAL_INFO!._P![i];
                    if (i == 0)
                    {
                        tech_prof_list += $"{element._Text}";
                    }
                    else if (i == (tech_prof_count - 1))
                    {
                        tech_prof_list += $"\n{element._Text}";
                    }
                    else
                    {
                        tech_prof_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(tech_prof_list);

                string perf_cond_list = "form_section.form.lefti.PERFORMANCE_CONDITIONS = ";
                int perf_cond_count = ted.FORM_SECTION!._FORM._LEFTI!._PERFORMANCE_CONDITIONS!._P!.Count();
                for (int i = 0; i < perf_cond_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._LEFTI!._PERFORMANCE_CONDITIONS!._P![i];
                    if (i == 0)
                    {
                        perf_cond_list += $"{element._Text}";
                    }
                    else if (i == (perf_cond_count - 1))
                    {
                        perf_cond_list += $"\n{element._Text}";
                    }
                    else
                    {
                        perf_cond_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(perf_cond_list);

                //COMPLEMENTARY INFO
                string ci_info_add_list = "form_section.form.compl_info.INFO_ADD = ";
                int ci_info_add_count = ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._INFO_ADD!._P!.Count();
                for (int i = 0; i < ci_info_add_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._INFO_ADD!._P![i];
                    if (i == 0)
                    {
                        ci_info_add_list += $"{element._Text}";
                    }
                    else
                    {
                        ci_info_add_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(ci_info_add_list);

                string review_procedure_list = "form_section.form.compl_info.REVIEW_PROCEDURE = ";
                int review_procedure_count = ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._REVIEW_PROCEDURE!._P!.Count();
                for (int i = 0; i < review_procedure_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._REVIEW_PROCEDURE!._P![i];
                    if (i == 0)
                    {
                        review_procedure_list += $"{element._Text}";
                    }
                    else if (i == (review_procedure_count - 1))
                    {
                        review_procedure_list += $"\n{element._Text}";
                    }
                    else
                    {
                        review_procedure_list += $"\n{element._Text}";
                    }
                }
                ConsoleUtils.WriteLineEventColor(review_procedure_list);

                ConsoleUtils.WriteLineEventColor($"form_section.form.compl_info.DATE_DISPATCH= " +
                    $"{ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._DATE_DISPATCH_NOTICE!._Text}");

                //ADDRESS_CONTRACTING_BODY esto a lo mejor no es interesante
                ConsoleUtils.WriteLineEventColor($"form_section.form.contracting_body.addr_contr_body.NAME = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._OFFICIALNAME!._Text}");
                ConsoleUtils.WriteLineEventColor($"form_section.form.contracting_body.addr_contr_body.ADDRESS/TOWN/POSTALCODE = " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._ADDRESS!._Text}, " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._TOWN!._Text}, " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._POSTAL_CODE!._Text}");
                ConsoleUtils.WriteLineEventColor($"form_section.form.contracting_body.addr_contr_body.URL_GENERAL = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._URL_GENERAL!._Text}");
                ConsoleUtils.WriteLineEventColor($"form_section.form.contracting_body.addr_contr_body.URL_BUYER = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._URL_BUYER!._Text}");

            }
        }

    }
}




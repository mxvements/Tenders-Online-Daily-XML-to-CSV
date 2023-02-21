using LeerTedXML.TXTCreate;
using LeerTedXML.XMLDocument;
using MyApp;
using System.Collections;
using System.Text;
using TedXMLExport.Menu;
using TedXMLExport.XMLTedObject;

namespace LeerTedXML.CSVCreate
{
    internal class TXTCreate : ITXTCreate
    {
        #region main attr and ctor
        // inyección de dependencias
        private readonly IXMLTedParser _ixmlDoc;        
        
        //attr
        private string? _txtFilePath { get; set; }
        public string _txtTitles {
            get 
            {
                return  "TED_OBJECT_ID/\t" +
                        "NOTICE_DATA/ NO_DOC_OJS/\t" +
                        "REF_OJ/ DATE_PUB/\t" +
                        "NOTICE_DELETION_DATE/\t" +
                        "NOTICE_DATA/ URI_LIST\t" +
                        "NOTICE_DATA/ LANGUAGE\t" +
                        "FORM/ CATEGORY-TYPE\t" +
                        "FORM/ DIRECTIVE\t" +
                        "TRANSLATION_SECTION/ CITY\t" +
                        "TRANSLATION_SECTION/ TOWN\t" +
                        "TRANSLATION_SECTION/ TEXT\t" +
                        "NOTICE_DATA/ CPV\t" +
                        "CODIF_DATA/ AA_AUTHORITY_TYPE\t" +
                        "CODIF_DATA/ TD_DOCUMENT_TYPE\t" +
                        "CODIF_DATA/ NC_CONTRACT_NATURE\t" +
                        "CODIF_DATA/ PR_PROC\t" +
                        "CODIF_DATA/ RP_REGULATION\t" +
                        "CODIF_DATA/ TY_TYPE_BID\t" +
                        "CODIF_DATA/ AC_AWARD_CRIT\t" +
                        "CODIF_DATA/ MA_MAIN_ACTIVITIES\t" +
                        "OBJECT_CONTRACT/ TITLE\t" +
                        "OBJECT_CONTRACT/ SHORT_DESCRIPTION\t" +
                        "CONTRACTING_BODY/ CA_TYPE\t" +
                        "CONTRACTING_BODY/ CA_TYPE_OTHER\t" +
                        "CONTRACTING_BODY/ CA_ACTIVITY\t" +
                        "CONTRACTNG_BODY/ URL_DOCUMENT\t" +
                        "CONTACTING_BODY/ URL_PARTICIPATION\t" +
                        "FORM/ PROCEDURE/ PROCEDURE_TYPE\t" +
                        "FORM/ PROCEDURE/ NB_PARTICIPANTS\t" +
                        "FORM/ PROCEDURE/ DATE_RECEIPT_TENDERS\t" +
                        "FORM/ PROCEDURE/ LANGUAGES\t" +
                        "FORM/ PROCEDURE/ NUMBER_VALUE_PRIZE\t" +
                        "FORM/ PROCEUDRE/ CRITERIA_EVALUATION\t" +
                        "FORM/ PROCEDURE/ DURATION_TENDER_VALID\t" +
                        "FORM/ LEFTI/ CRITERIA_SELECTION\t" +
                        "FORM/ LEFTI/ SUITABILITY\t" +
                        "FORM/ LEFTI/ PARTICULAR_PROFESSION\t" +
                        "FORM/ LEFTI/ ECONOMIC_FINANCIAL_INFO\t" +
                        "FORM/ LEFTI/ TECHNICAL_PROFESSIONAL_INFO\t" +
                        "FORM/ LEFTI/ PERFORMANCE_CONDITIONS\t" +
                        "FORM/ COMPLEMENTARY_INFO/ INFO_ADD\t" +
                        "FORM/ COMPLEMENTARY_INFO/ REVIEW_PROCEDURE\t" +
                        "FORM/ COMPLEMENTARY_INFO/ DATE_DISPATCH_NOTICE\t";
            }
        }

        //ctor
        public TXTCreate(IXMLTedParser ixmlDoc)
        {
            this._ixmlDoc = ixmlDoc;
        }
        #endregion

        //methods
        public void CreateNewTxt(int readOption)
        {
            // NOTE OPTION 2 FROM SUBMENU

            // cuando creamos uno nuevo tenemos que asignar ruta,            
            ConsoleUtils.WriteLineMenuColor("SET FOLDER path for new .txt:");
            String directoryInfo = new DirectoryInfo(Console.ReadLine()).ToString();

            // automatico, con la fecha
            DateTime current_datetime = DateTime.Now;
            string current_date = current_datetime.Year.ToString() + "-"
                + current_datetime.Month.ToString() + "-"
                + current_datetime.Day.ToString() + "-"
                + current_datetime.Hour.ToString() + "-"
                + current_datetime.Minute.ToString();

            string txt_name = $"TXT_TED-{current_date}.txt";

            this._txtFilePath = @"" + Path.Combine(directoryInfo, Path.GetFileName(txt_name));

            ConsoleUtils.WriteLineEventColor($"EVENT Name of new file {this._txtFilePath}");

            StringBuilder ted_export_list_string = TedExportToTxt(readOption);

            // create file and save on filepath
            FileInfo fi = new FileInfo(this._txtFilePath);
            try
            {
                if (fi.Exists) { fi.Delete(); }
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.WriteLine(this._txtTitles);
                    sw.WriteLine(ted_export_list_string);
                    sw.Close();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }

        public void AppendTxt(int readOption)
        {
            // NOTE OPTION 1 FROM SUBMENU

            // pedir ruta de archivo csv a actualizar
            ConsoleUtils.WriteLineMenuColor("SET .txt file path:");
            string input_directory = Console.ReadLine();
            string directory_info = string.Empty;

            try
            {
                if (File.Exists(input_directory))
                {
                    directory_info = new DirectoryInfo(input_directory).ToString();
                }
                else
                {
                    ConsoleUtils.WriteLineErrorColor($"ERROR wrong file path.");
                    return;
                }
            }catch(Exception ex)
            {
                ConsoleUtils.WriteLineErrorColor($"ERROR wrong file path. Error: {ex}");
            }

            this._txtFilePath = directory_info;

            // list of ted_export objects created
            StringBuilder ted_export_list_string = TedExportToTxt(readOption);
            FileInfo fi = new FileInfo(this._txtFilePath);
            try
            {
                using (StreamWriter sw = fi.AppendText())
                {
                    sw.WriteLine(ted_export_list_string);
                    sw.Close();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

        }

        public void ReadXml(int readOption)
        {
            // NOTE OPTION 3 FROM SUBMENU
            List<TED_EXPORT> ted_export_list = new List<TED_EXPORT>();

            // IF 1 = read folder and export
            // IF 2 = read single file and export
            if (readOption == 1) { ted_export_list = _ixmlDoc.XmlFolderFilesObjects(); }
            if (readOption == 2) { ted_export_list = _ixmlDoc.XmlSingleFileObject(); }

            PrintConsoleText(ted_export_list);
        }

        private string ConcatenateParagraph(List<P> pObjectList)
        {
            int pObjectCount = pObjectList.Count();
            string finalString = string.Empty;

            if(pObjectCount == 0) { return finalString; }

            for (int i = 0; i < pObjectCount; i++)
            {
                var element = pObjectList![i];
                if (i == 0)
                {
                    finalString += $"{element._Text!.Replace("\t", " // ").Replace("\n", " // ")}";
                }
                else
                {
                    finalString += $" // {element._Text!.Replace("\t", " // ").Replace("\n", " // ")}";
                }
            }

            return finalString;
        }

        private StringBuilder TedExportToTxt(int readOption)
        {
            StringBuilder ted_export_str = new StringBuilder();
            List<TED_EXPORT> ted_export_list = new List<TED_EXPORT>();

            // IF 1 = read folder and export
            // IF 2 = read single file and export
            if(readOption == 1){ ted_export_list = _ixmlDoc.XmlFolderFilesObjects(); }
            if(readOption == 2){ ted_export_list = _ixmlDoc.XmlSingleFileObject(); }
           
            int count = ted_export_list.Count;
            for(int k = 0; k<count; k++ )
            {
                //object to parse
                TED_EXPORT ted = ted_export_list[k];

                //add new row
                if (k != 0)
                {
                    ted_export_str.Append("\n");
                }

                // ID and dates
                ted_export_str.Append(ted.DOC_ID + "\t");
                ted_export_str.Append(ted.CODED_DATA_SECTION!._NOTICE_DATA!._NO_DOC_OJS!._Text + "\t");
                ted_export_str.Append(ted.CODED_DATA_SECTION!._REF_OJS!._DATE_PUB!._Text + "\t");
                ted_export_str.Append(ted.TECHNICAL_SECTION!._DELETION_DATE!._Text + "\t");

                // ted uri
                StringBuilder uri_docs = new StringBuilder();
                ted.CODED_DATA_SECTION!._NOTICE_DATA!._URI_LIST!._URI_DOC!.ForEach(x => {
                    uri_docs.AppendJoin(" // ", x._Text!.Replace("\n", ""));
                });
                ted_export_str.Append(uri_docs + "\t");

                // LANGUAAGE AND FORMS INFO
                ted_export_str.Append(ted.CODED_DATA_SECTION!._NOTICE_DATA!._LG_ORIG!._Text + "\t");
                ted_export_str.Append($"{ted.FORM_SECTION!._FORM!._CATEGORY} // {ted.FORM_SECTION!._FORM!._FORM}" + "\t");
                ted_export_str.Append(ted.FORM_SECTION!._FORM!._DIRECTIVE!._VALUE + "\t");

                // LOCALIZATION
                StringBuilder ti_cy = new StringBuilder();
                StringBuilder ti_towm = new StringBuilder();
                StringBuilder ti_text = new StringBuilder();
                ted.TRANSLATION_SECTION!._ML_TITLES!._ML_TI_DOC!.ForEach(x =>
                {
                    ti_cy.Append(x._TI_CY!._Text!.Replace("\n", ""));
                    ti_towm.Append(x._TI_TOWN!._Text!.Replace("\n", ""));
                    x._TI_TEXT!._P!.ForEach(y => ti_text.Append(y._Text!.Replace("\n", "")));
                });
                ted_export_str.Append(ti_cy + "\t");
                ted_export_str.Append(ti_towm + "\t");
                ted_export_str.Append(ti_text + "\t");

                // CPV
                StringBuilder original_cpv_list = new StringBuilder();
                ArrayList cpv_text_list = new ArrayList();
                ted.CODED_DATA_SECTION!._NOTICE_DATA!._ORIGINAL_CPV!.ForEach(x =>
                {
                    cpv_text_list.Add(x._CODE + "-" + x._Text!.Replace("\n", ""));
                });
                original_cpv_list.AppendJoin(" // ", cpv_text_list.ToArray());
                ted_export_str.Append(original_cpv_list + "\t");

                // CODIF_DATA
                ted_export_str.Append(
                     $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._AA_AUTHORITY_TYPE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._AA_AUTHORITY_TYPE!._Text}" + "\t");
                ted_export_str.Append(
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._TD_DOCUMENT_TYPE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._TD_DOCUMENT_TYPE!._Text}" + "\t");
                ted_export_str.Append(
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._NC_CONTRACT_NATURE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._NC_CONTRACT_NATURE!._Text}" + "\t");
                ted_export_str.Append(
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._PR_PROC!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._PR_PROC!._Text}" + "\t");
                ted_export_str.Append(
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._RP_REGULATION!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._RP_REGULATION!._Text}" + "\t");
                ted_export_str.Append(
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._TY_TYPE_BID!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._TY_TYPE_BID!._Text}" + "\t");
                ted_export_str.Append(
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._AC_AWARD_CRIT!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._AC_AWARD_CRIT!._Text}" + "\t");
                ted_export_str.Append(
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._MA_MAIN_ACTIVITIES!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._MA_MAIN_ACTIVITIES!._Text}" + "\t");

                // OBJECT_CONTRACT
                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM!._OBJECT_CONTRACT!._TITLE!._P!) + "\t");
                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._OBJECT_CONTRACT!._OBJECT_DESCR!._SHORT_DESCR!._P!) + "\t");

                // CONTRACTING_BODY
                ted_export_str.Append($"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_TYPE!._VALUE}" + "\t");
                ted_export_str.Append($"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_TYPE_OTHER!._Text}" + "\t");
                ted_export_str.Append($"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_ACTIVITY!._VALUE}" + "\t");
                ted_export_str.Append($"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._URL_DOCUMENT!._Text}" + "\t");
                ted_export_str.Append($"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._URL_PARTICIPATION!._Text}" + "\t");

                // PROCEDURE
                ted_export_str.Append($"{ted.FORM_SECTION!._FORM._PROCEDURE!._first_element!.ToUpper()}" + "\t");

                if (ted.FORM_SECTION!._FORM._PROCEDURE!._NB_PARTICIPANTS!._Text is not null)
                {
                    ted_export_str.Append($"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_PARTICIPANTS!._Text}" + "\t");
                }
                else
                {
                    ted_export_str.Append(
                         $"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_MIN_PARTICIPANTS!._Text} - " +
                         $"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_MAX_PARTICIPANTS!._Text}" + "\t");
                }

                ted_export_str.Append($"{ted.FORM_SECTION!._FORM._PROCEDURE!._DATE_RECEIPT_TENDERS!._Text} / "
                                      + $"{ted.FORM_SECTION!._FORM._PROCEDURE!._TIME_RECEIPT_TENDERS!._Text}" + "\t");

                StringBuilder pr_lang_list = new StringBuilder();
                ArrayList lang_text_list = new ArrayList();
                ted.FORM_SECTION!._FORM._PROCEDURE!._LANGUAGES!._LANGUAGE!.ForEach(x =>
                {
                    //no funciona el appendjoin
                    lang_text_list.Add(x._VALUE!.Replace("\n", ""));
                });
                pr_lang_list.AppendJoin(" // ", lang_text_list.ToArray());
                ted_export_str.Append(pr_lang_list + "\t");

                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._PROCEDURE!._NUMBER_VALUE_PRIZE!._P!) + "\t");
                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._PROCEDURE!._CRITERIA_EVALUATION!._P!) + "\t");

                if (ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._TYPE != String.Empty)
                {
                    ted_export_str.Append(
                        $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._Text} / "
                         + $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._TYPE}" + "\t");
                }
                else
                {
                    ted_export_str.Append($"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._Text}" + "\t");
                }

                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._CRITERIA_SELECTION!._P!) + "\t");
                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._SUITABILITY!._P!) + "\t");

                StringBuilder particular_prof_list_attr = new StringBuilder();
                if (ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._CTYPE! != String.Empty)
                {
                    particular_prof_list_attr.Append($"CTYPE: {ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._CTYPE!}/ ");
                }
                ted_export_str.Append(particular_prof_list_attr + ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._P!) + "\t");


                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._ECONOMIC_FINANCIAL_INFO!._P!) + "\t");
                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._TECHNICAL_PROFESSIONAL_INFO!._P!) + "\t");
                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._PERFORMANCE_CONDITIONS!._P!) + "\t");

                // COMPLEMENTARY INFO
                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._INFO_ADD!._P!) + "\t");
                ted_export_str.Append(ConcatenateParagraph(ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._REVIEW_PROCEDURE!._P!) + "\t");
                ted_export_str.Append($"{ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._DATE_DISPATCH_NOTICE!._Text}" + "\t");

            }

            return ted_export_str;
        }


        private static void PrintConsoleText(List<TED_EXPORT> ted_exportList)
        {
            ConsoleUtils.WriteLineInfoColor("INFO Console print:");
            //WIP just to check
            foreach (TED_EXPORT ted in ted_exportList)
            {
                StringBuilder console_output = new StringBuilder();

                console_output.AppendLine($"INFO Current object: {ted.DOC_ID}");

                //ID and dates
                console_output.AppendLine($"ted object ID: {ted.DOC_ID}");
                console_output.AppendLine($"coded_data_section.notice_data.NO_DOC_OJS = {ted.CODED_DATA_SECTION!._NOTICE_DATA!._NO_DOC_OJS!._Text}");
                console_output.AppendLine($"coded_data_section.ref_ojs.DATE_PUB= {ted.CODED_DATA_SECTION!._REF_OJS!._DATE_PUB!._Text}");
                console_output.AppendLine($"technical_section.DELETION_DATE = {ted.TECHNICAL_SECTION!._DELETION_DATE!._Text}");

                //ted uri
                ArrayList uri_doc_list = new ArrayList();
                StringBuilder uri_doc = new StringBuilder();
                ted.CODED_DATA_SECTION!._NOTICE_DATA!._URI_LIST!._URI_DOC!.ForEach(x =>
                {
                    uri_doc_list.Add(x._Text);
                });
                uri_doc.Append("coded_data_section.notice_data.uri_list.URI_DOC = ");
                uri_doc.AppendJoin(" // ", (string[])uri_doc_list.ToArray( typeof(string) ));
                console_output.AppendLine(uri_doc.ToString());

                //LANGUAAGE AND FORMS INFO
                console_output.AppendLine($"technical_section.notice_data.LG_ORIG = {ted.CODED_DATA_SECTION!._NOTICE_DATA!._LG_ORIG!._Text}");
                console_output.AppendLine($"form_section.form.CAT/FORM = " +
                    $"{ted.FORM_SECTION!._FORM!._CATEGORY} // {ted.FORM_SECTION!._FORM!._FORM}");
                console_output.AppendLine($"form_section.form.DIRECTIVE = {ted.FORM_SECTION!._FORM!._DIRECTIVE!._VALUE}");

                //LOCALIZATION
                StringBuilder ti_cy = new StringBuilder();
                ti_cy.Append("translation_section.ml_titles_ml_ti_doc_TI_CY = ");
                StringBuilder ti_town = new StringBuilder();
                ti_town.Append("translation_section.ml_titles_ml_ti_doc_TI_TOWN = ");
                StringBuilder ti_text = new StringBuilder();
                ti_text.Append("translation_section.ml_titles_ml_ti_doc_TI_TEXT= ");
                ted.TRANSLATION_SECTION!._ML_TITLES!._ML_TI_DOC!.ForEach(x =>
                {
                    ti_cy.Append(x._TI_CY!._Text!.Replace("\n", ""));
                    ti_town.Append(x._TI_TOWN!._Text!.Replace("\n", ""));
                    x._TI_TEXT!._P!.ForEach(y =>
                    {
                        ti_text.Append(y._Text!.Replace("\n", ""));
                    });
                });
                console_output.AppendLine(ti_cy.ToString());
                console_output.AppendLine(ti_town.ToString());
                console_output.AppendLine(ti_text.ToString());

                //CPV
                ArrayList original_cpv_list = new ArrayList();
                StringBuilder original_cpv = new StringBuilder();
                ted.CODED_DATA_SECTION!._NOTICE_DATA!._ORIGINAL_CPV!.ForEach(X =>
                {
                    original_cpv_list.Add(X._Text!.Replace("\n", ""));
                });
                original_cpv.Append("coded_data_section.notice_data.ORIGINAL_CPV = ");
                original_cpv.AppendJoin(" // ", (string[])original_cpv_list.ToArray(typeof(string)));
                console_output.AppendLine(original_cpv.ToString());

                //CODIF_DATA
                console_output.AppendLine($"technical_section.codif_data._AA_AUTHORITY_TYPE = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._AA_AUTHORITY_TYPE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._AA_AUTHORITY_TYPE!._Text}");
                console_output.AppendLine($"technical_section.codif_data._TD_DOCUMENT_TYP = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._TD_DOCUMENT_TYPE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._TD_DOCUMENT_TYPE!._Text}");
                console_output.AppendLine($"technical_section.codif_data._NC_CONTRACT_NATURE = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._NC_CONTRACT_NATURE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._NC_CONTRACT_NATURE!._Text}");
                console_output.AppendLine($"technical_section.codif_data._PR_PROC = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._PR_PROC!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._PR_PROC!._Text}");
                console_output.AppendLine($"technical_section.codif_data._RP_REGULATION = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._RP_REGULATION!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._RP_REGULATION!._Text}");
                console_output.AppendLine($"technical_section.codif_data._TY_TYPE_BID = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._TY_TYPE_BID!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._TY_TYPE_BID!._Text}");
                console_output.AppendLine($"technical_section.codif_data._AC_AWARD_CRIT = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._AC_AWARD_CRIT!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._AC_AWARD_CRIT!._Text}");
                console_output.AppendLine($"technical_section.codif_data._MA_MAIN_ACTIVITIES = " +
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._MA_MAIN_ACTIVITIES!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._MA_MAIN_ACTIVITIES!._Text}");

                //OBJECT_CONTRACT
                ArrayList ob_title_list = new ArrayList();
                StringBuilder ob_title = new StringBuilder();
                ted.FORM_SECTION!._FORM!._OBJECT_CONTRACT!._TITLE!._P!.ForEach(x =>
                {
                    ob_title_list.Add(x._Text.Replace("\n", ""));
                });
                ob_title.Append("form_section.form.object_contract.TITLE = ");
                ob_title.AppendJoin(" // ", (string[])ob_title_list.ToArray(typeof(string)));
                console_output.AppendLine(ob_title.ToString());

                ArrayList ob_shortdescr_list = new ArrayList();
                StringBuilder ob_shortdescr = new StringBuilder();
                ted.FORM_SECTION!._FORM._OBJECT_CONTRACT!._OBJECT_DESCR!._SHORT_DESCR!._P!.ForEach(x =>
                {
                    ob_shortdescr_list.Add(x._Text);
                });
                ob_shortdescr.Append("form_section.form.object_contract.SHORT_DESCR = ");
                ob_shortdescr.AppendJoin(" // ", (string[])ob_shortdescr_list.ToArray(typeof(string)));
                console_output.AppendLine(ob_shortdescr.ToString());

                //CONTRACTING_BODY
                console_output.AppendLine($"form_section.form.contracting_body.CA_TYPE = " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_TYPE!._VALUE}");
                console_output.AppendLine($"form_section.form.contracting_body.CA_TYPE_OTHER = " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_TYPE_OTHER!._Text}");
                console_output.AppendLine($"form_section.form.contracting_body.CA_ACTIVITY = " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_ACTIVITY!._VALUE}");
                console_output.AppendLine($"form_section.form.contracting_body.URL_DOCUMENT = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._URL_DOCUMENT!._Text}");
                console_output.AppendLine($"form_section.form.contracting_body.URL_PARTICIPATION = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._URL_PARTICIPATION!._Text}");

                //PROCEDURE
                console_output.AppendLine($"form_section.form.procedure.PROCEDURE_TYPE = " +
                   $"{ted.FORM_SECTION!._FORM._PROCEDURE!._first_element!.ToUpper()}");

                if (ted.FORM_SECTION!._FORM._PROCEDURE!._NB_PARTICIPANTS!._Text is not null)
                {
                    console_output.AppendLine($"form_section.form.procedure.PARTICIPANTS = " +
                   $"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_PARTICIPANTS!._Text}");
                }
                else
                {
                    console_output.AppendLine($"form_section.form.procedure.PARTICIPANTS = " +
                       $"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_MIN_PARTICIPANTS!._Text} - {ted.FORM_SECTION!._FORM._PROCEDURE!._NB_MAX_PARTICIPANTS!._Text}");
                }

                console_output.AppendLine($"form_section.form.procedure.DATE_RECEIPT_DATE = " +
                  $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DATE_RECEIPT_TENDERS!._Text} / {ted.FORM_SECTION!._FORM._PROCEDURE!._TIME_RECEIPT_TENDERS!._Text}");

                ArrayList pr_lang_list = new ArrayList();
                StringBuilder pr_lang = new StringBuilder();
                ted.FORM_SECTION!._FORM._PROCEDURE!._LANGUAGES!._LANGUAGE!.ForEach(lang =>
                {
                    pr_lang_list.Add(lang._VALUE);
                });
                pr_lang.Append("form_section.form.procedure.LANG = ");
                pr_lang.AppendJoin(" // ", (string[])pr_lang_list.ToArray(typeof(string)));
                console_output.AppendLine(pr_lang.ToString());

                ArrayList num_val_price_list = new ArrayList();
                StringBuilder num_val_price = new StringBuilder();
                ted.FORM_SECTION!._FORM._PROCEDURE!._NUMBER_VALUE_PRIZE!._P!.ForEach(x =>
                {
                    num_val_price_list.Add(x._Text!);
                });
                num_val_price.Append("form_section.form.procedure.NUMBER_VALUE_PRICE = ");
                num_val_price.AppendJoin(" // ", (string[])num_val_price_list.ToArray(typeof(string)));
                console_output.AppendLine(num_val_price.ToString());

                ArrayList criteria_ev_list = new ArrayList();
                StringBuilder criteria_ev = new StringBuilder();
                ted.FORM_SECTION!._FORM._PROCEDURE!._CRITERIA_EVALUATION!._P!.ForEach(x =>
                {
                    criteria_ev_list.Add(x._Text!.Replace("\n", ""));
                });
                criteria_ev.Append("form_section.form.procedure.CRITERIA_EVAL = ");
                criteria_ev.AppendJoin(" // ", (string[])criteria_ev_list.ToArray(typeof(string)));
                console_output.AppendLine(criteria_ev.ToString());

                if (ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._TYPE != String.Empty)
                {
                    console_output.AppendLine($"form_section.form.procedure.DURATION_TENDER = " +
                       $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._Text} / {ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._TYPE}");
                }
                else
                {
                    console_output.AppendLine($"form_section.form.procedure.DURATION_TENDER = " +
                   $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._Text}");
                }

                //LEFTI
                ArrayList criteria_sel_list = new ArrayList();
                StringBuilder criteria_sel = new StringBuilder();
                ted.FORM_SECTION!._FORM._LEFTI!._CRITERIA_SELECTION!._P!.ForEach(x =>
                {
                    criteria_sel_list.Add(x._Text!.Replace("\n", ""));
                });
                criteria_sel.Append("form_section.form.lefti.CRITERIA_SELECTION = ");
                criteria_sel.AppendJoin(" // ", (string[])criteria_sel_list.ToArray(typeof(string)));
                console_output.AppendLine(criteria_sel.ToString());

                ArrayList suitability_list = new ArrayList();
                StringBuilder suitability = new StringBuilder();
                ted.FORM_SECTION!._FORM._LEFTI!._SUITABILITY!._P!.ForEach(x =>
                {
                    suitability_list.Add(x._Text!.Replace("\n", ""));
                });
                suitability.Append("form_section.form.lefti.SUITABILITY = ");
                suitability.AppendJoin(" // ", (string[])suitability_list.ToArray(typeof(string)));
                console_output.AppendLine(suitability.ToString());

                ArrayList particular_prof_list = new ArrayList();
                StringBuilder particular_prof = new StringBuilder();
                ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._P!.ForEach(x =>
                {
                    particular_prof_list.Add(x._Text!.Replace("\n", ""));
                });
                particular_prof.Append("form_section.form.lefti.PARTICULAR_PROF = ");
                particular_prof.AppendJoin(" // ", (string[])particular_prof_list.ToArray(typeof(string)));
                console_output.AppendLine(particular_prof.ToString());

                ArrayList economic_fin_info_list = new ArrayList();
                StringBuilder economic_fin_info = new StringBuilder();
                ted.FORM_SECTION!._FORM._LEFTI!._ECONOMIC_FINANCIAL_INFO!._P!.ForEach(x =>
                {
                    economic_fin_info_list.Add(x._Text!.Replace("\n", ""));
                });
                economic_fin_info.Append("form_section.form.lefti.ECONOMIC_FINANCIAL_INFO = ");
                economic_fin_info.AppendJoin(" // ", (string[])economic_fin_info_list.ToArray(typeof(string)));
                console_output.AppendLine(economic_fin_info.ToString());

                ArrayList tech_prof_list = new ArrayList();
                StringBuilder tech_prof = new StringBuilder();
                ted.FORM_SECTION!._FORM._LEFTI!._TECHNICAL_PROFESSIONAL_INFO!._P!.ForEach(x =>
                { 
                    tech_prof_list.Add(x._Text!.Replace("\n", ""));
                });
                tech_prof.Append("form_section.form.lefti.TECHNICAL_PROFESIONAL_INFO = ");
                tech_prof.AppendJoin(" // ", (string[])tech_prof_list.ToArray(typeof(string)));
                console_output.AppendLine(tech_prof.ToString());

                ArrayList perf_cond_list = new ArrayList();
                StringBuilder perf_cond = new StringBuilder();
                ted.FORM_SECTION!._FORM._LEFTI!._PERFORMANCE_CONDITIONS!._P!.ForEach(x =>
                {
                    perf_cond_list.Add(x._Text!.Replace("\n", ""));
                });
                perf_cond.Append("form_section.form.lefti.PERFORMANCE_CONDITIONS = ");
                perf_cond.AppendJoin(" // ", (string[])tech_prof_list.ToArray(typeof(string)));
                console_output.AppendLine(perf_cond.ToString());

                //COMPLEMENTARY INFO

                ArrayList ci_info_add_list = new ArrayList();
                StringBuilder ci_info_add = new StringBuilder();
                ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._INFO_ADD!._P!.ForEach(x => { ci_info_add_list.Add(x._Text!.Replace("\n", "")); });
                ci_info_add.Append("form_section.form.compl_info.INFO_ADD = ");
                ci_info_add.AppendJoin(" // ", (string[])ci_info_add_list.ToArray(typeof(string)));
                console_output.AppendLine(ci_info_add.ToString());

                console_output.AppendLine($"form_section.form.compl_info.DATE_DISPATCH= " +
                    $"{ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._DATE_DISPATCH_NOTICE!._Text}");

                //ADDRESS_CONTRACTING_BODY esto a lo mejor no es interesante
                console_output.AppendLine($"form_section.form.contracting_body.addr_contr_body.NAME = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._OFFICIALNAME!._Text}");
                console_output.AppendLine($"form_section.form.contracting_body.addr_contr_body.ADDRESS/TOWN/POSTALCODE = " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._ADDRESS!._Text}, " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._TOWN!._Text}, " +
                   $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._POSTAL_CODE!._Text}");
                console_output.AppendLine($"form_section.form.contracting_body.addr_contr_body.URL_GENERAL = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._URL_GENERAL!._Text}");
                console_output.Append($"form_section.form.contracting_body.addr_contr_body.URL_BUYER = " +
                    $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._ADDRESS_CONTRACTING_BODY!._URL_BUYER!._Text}");

                Console.WriteLine(console_output.ToString());
            }
        }

    }
}

using LeerTedXML.TXTCreate;
using LeerTedXML.XMLDocument;
using MyApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TedXMLExport.Menu;
using TedXMLExport.XMLTedObject;

namespace LeerTedXML.CSVCreate
{
    internal class TXTCreate : ITXTCreate
    {
        // inyección de dependencias
        private readonly IXMLDoc _ixmlDoc;
        
        //attributos
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

        public TXTCreate(IXMLDoc ixmlDoc)
        {
            this._ixmlDoc = ixmlDoc;
        }

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

            string ted_export_list_string = TedExportToTxt(readOption);

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
            // NOTE 1 FROM SUBMENU

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
            string ted_export_list_string = TedExportToTxt(readOption);
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

            Program.PrintControlText(ted_export_list);
        }

        public static string ConcatenateParagraph(List<P> pObjectList)
        {
            int pObjectCount = pObjectList.Count();
            string finalString = string.Empty;

            if(pObjectCount == 0) { return finalString; }

            for (int i = 0; i < pObjectCount; i++)
            {
                var element = pObjectList![i];
                if (i == 0)
                {
                    finalString += $"{element._Text.Replace("\t", " // ").Replace("\n", " // ")}";
                }
                else
                {
                    finalString += $" // {element._Text.Replace("\t", " // ").Replace("\n", " // ")}";
                }
            }

            return finalString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readOption"></param>
        /// <returns></returns>
        private string TedExportToTxt(int readOption)
        {
            List<TED_EXPORT> ted_export_list = new List<TED_EXPORT>();
            string ted_export_string = string.Empty;

            StringBuilder ted_export_stringbuilder = new StringBuilder();

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
                if (k != 0){ ted_export_string += "\n"; }

                // ID and dates
                ted_export_string += ted.DOC_ID + "\t";
                ted_export_string += ted.CODED_DATA_SECTION!._NOTICE_DATA!._NO_DOC_OJS!._Text + "\t";
                ted_export_string += ted.CODED_DATA_SECTION!._REF_OJS!._DATE_PUB!._Text + "\t";
                ted_export_string += ted.TECHNICAL_SECTION!._DELETION_DATE!._Text + "\t";

                // ted uri
                string uri_list = string.Empty;
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
                        uri_list += $"{element._Text} // ";
                    }

                }
                ted_export_string += uri_list + "\t";

                // LANGUAAGE AND FORMS INFO
                ted_export_string += ted.CODED_DATA_SECTION!._NOTICE_DATA!._LG_ORIG!._Text + "\t";
                ted_export_string += $"{ted.FORM_SECTION!._FORM!._CATEGORY} // {ted.FORM_SECTION!._FORM!._FORM}" + "\t";
                ted_export_string += ted.FORM_SECTION!._FORM!._DIRECTIVE!._VALUE + "\t";

                // LOCALIZATION
                int ml_ti_doc_count = ted.TRANSLATION_SECTION!._ML_TITLES!._ML_TI_DOC!.Count();
                string ti_cy = string.Empty;
                string ti_towm = string.Empty;
                string ti_text = string.Empty;
                for (int i = 0; i < ml_ti_doc_count; i++)
                {
                    var element = ted.TRANSLATION_SECTION!._ML_TITLES!._ML_TI_DOC![i];
                    if (i == (ml_ti_doc_count - 1))
                    {
                        ti_cy += $"{element._TI_CY!._Text}";
                        ti_towm += $"{element._TI_TOWN!._Text}";

                        int ml_ti_text_p_count = element._TI_TEXT!._P!.Count();
                        for (int j = 0; j < ml_ti_text_p_count; j++)
                        {
                            var el = ted.TRANSLATION_SECTION!._ML_TITLES!._ML_TI_DOC![i]._TI_TEXT!._P![j];
                            ti_text += $"{el._Text}";
                        }
                    }
                }
                ted_export_string += ti_cy + "\t";
                ted_export_string += ti_towm + "\t";
                ted_export_string += ti_text + "\t";

                // CPV
                string original_cpv_list = string.Empty;
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
                        original_cpv_list += $"{element._CODE} - {element._Text} // ";
                    }
                }
                ted_export_string += original_cpv_list + "\t";

                // CODIF_DATA
                ted_export_string +=
                     $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._AA_AUTHORITY_TYPE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._AA_AUTHORITY_TYPE!._Text}" + "\t";
                ted_export_string +=
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._TD_DOCUMENT_TYPE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._TD_DOCUMENT_TYPE!._Text}" + "\t";
                ted_export_string +=
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._NC_CONTRACT_NATURE!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._NC_CONTRACT_NATURE!._Text}" + "\t";
                ted_export_string +=
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._PR_PROC!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._PR_PROC!._Text}" + "\t";
                ted_export_string +=
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._RP_REGULATION!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._RP_REGULATION!._Text}" + "\t";
                ted_export_string +=
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._TY_TYPE_BID!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._TY_TYPE_BID!._Text}" + "\t";
                ted_export_string +=
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._AC_AWARD_CRIT!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._AC_AWARD_CRIT!._Text}" + "\t";
                ted_export_string +=
                    $"{ted.CODED_DATA_SECTION!._CODIF_DATA!._MA_MAIN_ACTIVITIES!._CODE} - {ted.CODED_DATA_SECTION!._CODIF_DATA!._MA_MAIN_ACTIVITIES!._Text}" + "\t";

                // OBJECT_CONTRACT
                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM!._OBJECT_CONTRACT!._TITLE!._P!) + "\t";
                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._OBJECT_CONTRACT!._OBJECT_DESCR!._SHORT_DESCR!._P!) + "\t";

                // CONTRACTING_BODY
                ted_export_string += $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_TYPE!._VALUE}" + "\t";
                ted_export_string += $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_TYPE_OTHER!._Text}" + "\t";
                ted_export_string += $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._CA_ACTIVITY!._VALUE}" + "\t";
                ted_export_string += $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._URL_DOCUMENT!._Text}" + "\t";
                ted_export_string += $"{ted.FORM_SECTION!._FORM._CONTRACTING_BODY!._URL_PARTICIPATION!._Text}" + "\t";

                // PROCEDURE
                ted_export_string += $"{ted.FORM_SECTION!._FORM._PROCEDURE!._first_element!.ToUpper()}" + "\t";

                if (ted.FORM_SECTION!._FORM._PROCEDURE!._NB_PARTICIPANTS!._Text is not null)
                {
                    ted_export_string += $"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_PARTICIPANTS!._Text}" + "\t";
                }
                else
                {
                    ted_export_string +=
                         $"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_MIN_PARTICIPANTS!._Text} - " +
                         $"{ted.FORM_SECTION!._FORM._PROCEDURE!._NB_MAX_PARTICIPANTS!._Text}" + "\t";
                }

                ted_export_string += $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DATE_RECEIPT_TENDERS!._Text} / " +
                                     $"{ted.FORM_SECTION!._FORM._PROCEDURE!._TIME_RECEIPT_TENDERS!._Text}" + "\t";

                string pr_lang_list = string.Empty;
                int pr_lang_count = ted.FORM_SECTION!._FORM._PROCEDURE!._LANGUAGES!._LANGUAGE!.Count();
                for (int i = 0; i < pr_lang_count; i++)
                {
                    var element = ted.FORM_SECTION!._FORM._PROCEDURE!._LANGUAGES._LANGUAGE![i];
                    if (i == 0)
                    {
                        pr_lang_list += $"{element._VALUE.Replace("\t", " // ").Replace("\n", " // ")}";
                    }
                    else
                    {
                        pr_lang_list += $" // {element._VALUE.Replace("\t", " // ").Replace("\n", " // ")}";
                    }
                }
                ted_export_string += pr_lang_list + "\t";

                
                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._PROCEDURE!._NUMBER_VALUE_PRIZE!._P!) + "\t";
                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._PROCEDURE!._CRITERIA_EVALUATION!._P!) + "\t";

                if (ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._TYPE != String.Empty)
                {
                    ted_export_string +=
                        $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._Text} / " +
                        $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._TYPE}" + "\t";
                }
                else
                {
                    ted_export_string += $"{ted.FORM_SECTION!._FORM._PROCEDURE!._DURATION_TENDER_VALID!._Text}" + "\t";
                }

                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._CRITERIA_SELECTION!._P!) + "\t";
                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._SUITABILITY!._P!) + "\t";

                string particular_prof_list_attr = string.Empty;
                if (ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._CTYPE! != String.Empty)
                {
                    particular_prof_list_attr += $"CTYPE: {ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._CTYPE!}/ ";
                }
                ted_export_string += particular_prof_list_attr + ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._PARTICULAR_PROFESSION!._P!) + "\t";


                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._ECONOMIC_FINANCIAL_INFO!._P!) + "\t";
                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._TECHNICAL_PROFESSIONAL_INFO!._P!) + "\t";
                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._LEFTI!._PERFORMANCE_CONDITIONS!._P!) + "\t";

                // COMPLEMENTARY INFO
                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._INFO_ADD!._P!) + "\t";
                ted_export_string += ConcatenateParagraph(ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._REVIEW_PROCEDURE!._P!) + "\t";
                ted_export_string += $"{ted.FORM_SECTION!._FORM._COMPLEMENTARY_INFO!._DATE_DISPATCH_NOTICE!._Text}" + "\t";
                
                


            }

            return ted_export_string;
        }

       

    }
}

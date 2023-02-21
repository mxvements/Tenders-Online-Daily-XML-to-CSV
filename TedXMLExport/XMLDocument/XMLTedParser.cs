using LeerTedXML.XMLReader;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using TedXMLExport.Menu;
using TedXMLExport.XMLTedObject;

namespace LeerTedXML.XMLDocument
{
    internal class XMLTedParser : IXMLTedParser
    {
        //inyeccion de dependencias (XMLRead)
        private readonly IXMLTedFilter _ixmlRead;
        //ctor
        public XMLTedParser(IXMLTedFilter ixmlRead)
        {
            this._ixmlRead = ixmlRead;
        }


        //METHODS
        public List<TED_EXPORT> XmlFolderFilesObjects()
        {
            List<TED_EXPORT> ted_exportList = new List<TED_EXPORT>();
            List<string> pathList = _ixmlRead.ReadXmlFolder();
            ted_exportList = IterateFiles(pathList);

            return ted_exportList;
        }
        public List<TED_EXPORT> XmlSingleFileObject()
        {
            List<TED_EXPORT> ted_exportList= new List<TED_EXPORT>();
            List<string> pathList = _ixmlRead.ReadXmlSingleFile();
            ted_exportList = IterateFiles(pathList);
            
            return ted_exportList;
        }

        private List<TED_EXPORT> IterateFiles(List<string> pathList)
        {
            int numObjects = pathList.Count;
            List<TED_EXPORT> ted_exportList = new List<TED_EXPORT>();

            //para cada path recibido habrá que crear un ted_export object y añadirlo a la lista
            for (int i=0; i < numObjects;i++)
            {
                TED_EXPORT ted_export = new TED_EXPORT();
                string dirPath = pathList[i].ToString();
                
                ConsoleUtils.WriteLineEventColor("EVENT on XMLDoc. Reading Path:");
                Console.WriteLine($"{dirPath}");
                
                //using XDocument to read file
                XDocument xDocument = XDocument.Load(dirPath);

                //if its null, then continue loop
                if (xDocument.Root is null)
                {
                    continue;
                }

                //document attributes
                ted_export.Xmlns = xDocument.Root.Attribute("xmlns")!.Value;
                ted_export.VERSION = xDocument.Root.Attribute("VERSION")!.Value;
                ted_export.DOC_ID = xDocument.Root.Attribute("DOC_ID")!.Value;
                ted_export.EDITION = xDocument.Root.Attribute("EDITION")!.Value;

                XNamespace ns = ted_export.Xmlns;

                //using XmlDocument to get name of node
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(dirPath);
                XmlReader xmlReader = XmlReader.Create(dirPath);

                XmlNodeList rootChildren = xmlDoc.DocumentElement!.ChildNodes;
                foreach(XmlNode node in rootChildren)
                {
                    //cada node es TECHNICAL_SECTION, LINKS_SECTION, CODED_DATA_SECTION, TRANSLATION_SECTION y FORM_SECTION
                    switch (node.Name)
                    {
                        case "TECHCNICAL_SECTION":
                            ted_export.TECHNICAL_SECTION = ReadTechnicalSection(ns, xDocument);
                            continue;
                        case "CODED_DATA_SECTION":
                            ted_export.CODED_DATA_SECTION = ReadCodedDataSection(ns, xDocument);
                            continue;
                        case "TRANSLATION_SECTION":
                            ted_export.TRANSLATION_SECTION = ReadTranslationSection(ns, xDocument);
                            continue;
                        case "FORM_SECTION":
                            //extraer form_type
                            XmlNodeList form_section_children = node.ChildNodes;
                            String form_type = string.Empty;
                            int count = form_section_children.Count;
                            int form_type_node_index = 0;
                            
                            for(int j = 0; j<count; j++)
                            {
                                if (form_section_children[j]!.Name.StartsWith("F") )
                                {
                                    form_type_node_index = j;
                                }
                            }

                            XmlNode form_type_node = form_section_children[form_type_node_index];
                            form_type = form_type_node.Name;

                            ted_export.FORM_SECTION = ReadFormSection(ns, xDocument, form_type_node, form_type);
                            continue;
                    }
                }                

                ted_exportList.Add(ted_export);
            }

            return ted_exportList;
        }


        //metodos accesorios
        private List<P> CreatePList(IEnumerable<XElement> root)
        {
            List<P> plist = new List<P>();
            try
            {
                foreach (XElement xelem in root)
                {
                    P p = new P(xelem);
                    p._Text = p._Text!.Replace("\n", "");
                    plist.Add(p);
                }
            }
            catch (NullReferenceException) { }

            return plist;
        }
        private List<URI_DOC> CreateUriDocList(IEnumerable<XElement> root)
        {
            List<URI_DOC> uri_doc_list = new List<URI_DOC>();
            try
            {
                foreach (XElement uri_doc_xelem in root)
                {
                    URI_DOC uri_doc = new URI_DOC(uri_doc_xelem);
                    uri_doc._Text = uri_doc._Text!.Replace("\n","");
                    uri_doc_list.Add(uri_doc);
                }
            }
            catch (NullReferenceException) { }

            return uri_doc_list;
        }
        private List<VALUE> CreateValueList(IEnumerable<XElement> root)
        {
            List<VALUE> value_list = new List<VALUE>();
            try
            {
                foreach (XElement xelem in root)
                {
                    VALUE value = new VALUE(xelem);
                    value_list.Add(value);
                }
            }
            catch (NullReferenceException) { }

            return value_list;
        }
        private List<ORIGINAL_CPV> CreateOriginalCpvList(IEnumerable<XElement> root)
        {
            List<ORIGINAL_CPV> original_cpv_list = new List<ORIGINAL_CPV>();
            try
            {
                foreach (XElement xelem in root)
                {
                    ORIGINAL_CPV original_cpv = new ORIGINAL_CPV(xelem);
                    original_cpv._Text = original_cpv._Text.Replace("\n", "");
                    original_cpv_list.Add(original_cpv);
                }
            }
            catch (NullReferenceException) { }

            return original_cpv_list;
        }
        private List<AA_NAME> CreateAANameList(IEnumerable<XElement> root)
        {
            List<AA_NAME> aa_name_list = new List<AA_NAME>();
            try
            {
                foreach (XElement xelem in root)
                {
                    AA_NAME aa_name = new AA_NAME(xelem);
                    aa_name._Text = aa_name._Text.Replace("\n", "");
                    aa_name_list.Add(aa_name);
                }
            }
            catch (NullReferenceException) { }

            return aa_name_list;
        }
        private List<CPV_ADDITIONAL> CreateCpvAdditionalList(IEnumerable<XElement> root, XNamespace ns)
        {
            List<CPV_ADDITIONAL> cpv_additional_list = new List<CPV_ADDITIONAL>();
            try
            {
                foreach (XElement xelem in root)
                {
                    List<CPV_CODE> cpv_code_list = CreateCpvCodeList(root.Elements(ns + "CPV_CODE")!);
                    CPV_ADDITIONAL cpv_additional = new CPV_ADDITIONAL() { _CPV_CODE_LIST = cpv_code_list};
                    cpv_additional_list.Add(cpv_additional);
                }
            }
            catch (NullReferenceException) { }

            return cpv_additional_list;
        }
        private List<CPV_CODE> CreateCpvCodeList(IEnumerable<XElement> root)
        {
            List<CPV_CODE> cpv_code_list = new List<CPV_CODE>();
            try
            {
                foreach (XElement xelem in root)
                {
                    CPV_CODE cpv_code = new CPV_CODE(xelem);
                    cpv_code_list.Add(cpv_code);
                }
            }
            catch (NullReferenceException) { }

            return cpv_code_list;
        }
        private List<LANGUAGE> CreateLanguageList(IEnumerable<XElement> root)
        {
            List<LANGUAGE> language_list = new List<LANGUAGE>();
            foreach (XElement xelem in root)
            {
                LANGUAGE lang = new LANGUAGE(xelem);
                language_list.Add(lang);
            }
            return language_list;
        }
        private List<SINGLE_OPERATOR> CreateSingleOperatorList(IEnumerable<XElement> root)
        {
            List<SINGLE_OPERATOR> single_operator_list = new List<SINGLE_OPERATOR>();
            foreach (XElement xelem in root)
            {
                SINGLE_OPERATOR single_operator = new SINGLE_OPERATOR(xelem);
                single_operator._Text = single_operator._Text!.Replace("\n", "");
                single_operator_list.Add(single_operator);
            }
            return single_operator_list;
        }


        //metodos secciones ppales
        //1
        private TECHNICAL_SECTION ReadTechnicalSection(XNamespace ns, XDocument xDocument)
        {
            String tc = "TECHNICAL_SECTION";

            RECEPTION_ID reception_id = new RECEPTION_ID(
                xDocument.Root!.Element(ns + tc)!.Element(ns + "RECEPTION_ID")!);
            DELETION_DATE deletion_date = new DELETION_DATE(
                xDocument.Root!.Element(ns + tc)!.Element(ns + "DELETION_DATE")!);
            FORM_LG_LIST form_lg_list = new FORM_LG_LIST(
                xDocument.Root!.Element(ns + tc)!.Element(ns + "FORM_LG_LIST")!);
            COMMENTS comments = new COMMENTS(
                xDocument.Root!.Element(ns + tc)!.Element(ns + "COMMENTS")!);

            //TECHNICAL_SECTION
            TECHNICAL_SECTION technical_section = new TECHNICAL_SECTION
            {
                _RECEPTION_ID = reception_id,
                _DELETION_DATE = deletion_date,
                _FORM_LG_LIST = form_lg_list,
                _COMMENTS = comments
            };

            return technical_section;
        }

        private REF_OJS ReadRefOjs(XNamespace ns, XDocument xDocument, String parent)
        {
            String ro = "REF_OJS";

            COLL_OJ coll_oj = new COLL_OJ(
                xDocument.Root!.Element(ns + parent)!.Element(ns + ro)!.Element(ns + "COLL_OJ")!);
            NO_OJ no_oj = new NO_OJ(
                xDocument.Root.Element(ns + parent)!.Element(ns + ro)!.Element(ns + "NO_OJ")!);
            DATE_PUB date_pub = new DATE_PUB(
                xDocument.Root.Element(ns + parent)!.Element(ns + ro)!.Element(ns + "DATE_PUB")!);
           
            REF_OJS ref_obj = new()
            {
                _COLL_OJ = coll_oj,
                _NO_OJ = no_oj,
                _DATE_PUB = date_pub
            };

            return ref_obj;
        }
        private NOTICE_DATA ReadNoticeData(XNamespace ns, XDocument xDocument, String parent)
        {
            String nd = "NOTICE_DATA";

            NO_DOC_OJS no_doc_ojs = new(
                xDocument.Root!.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "NO_DOC_OJS")!);
            LG_ORIG lg_orig = new LG_ORIG(
                xDocument.Root!.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "LG_ORIG")!);
            ISO_COUNTRY iso_country = new ISO_COUNTRY(
                xDocument.Root!.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "ISO_COUNTRY")!);
            IA_URL_GENERAL ia_url_general = new IA_URL_GENERAL(
                xDocument.Root!.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "IA_URL_GENERAL")!);
            IA_URL_ETENDERING ia_url_etendering = new IA_URL_ETENDERING(
                xDocument.Root!.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "IA_URL_ETENDERING")!);
            ORIGINAL_NUTS original_nuts = new ORIGINAL_NUTS(
                xDocument.Root!.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "ORIGINAL_NUTS")!);
            CA_CE_NUTS ca_ce_nuts = new CA_CE_NUTS(
                xDocument.Root!.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "CA_CE_NUTS")!);
            PERFORMANCE_NUTS performance_nuts = new PERFORMANCE_NUTS(
                xDocument.Root!.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "PERFORMANCE_NUTS")!);
            List<ORIGINAL_CPV> original_cpv_list = CreateOriginalCpvList(
                xDocument.Root.Element(ns + parent)!.Element(ns + nd)!.Elements(ns + "ORIGINAL_CPV")!);

            URI_LIST uri_list = new URI_LIST();
            try
            {
                uri_list._URI_DOC = CreateUriDocList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "URI_LIST")!.Elements(ns + "URI_DOC")!);

            }
            catch (NullReferenceException) { };

            VALUES values = new VALUES();
            try
            {
                values._VALUE = CreateValueList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + nd)!.Element(ns + "VALUES")!.Elements(ns + "VALUE")!);
            }
            catch (NullReferenceException) { };

            NOTICE_DATA notice_data = new()
            {
                _NO_DOC_OJS = no_doc_ojs,
                _LG_ORIG = lg_orig,
                _ISO_COUNTRY = iso_country,
                _IA_URL_GENERAL = ia_url_general,
                _IA_URL_ETENDERING = ia_url_etendering,
                _ORIGINAL_CPV = original_cpv_list,
                _ORIGINAL_NUTS = original_nuts,
                _CA_CE_NUTS = ca_ce_nuts,
                _PERFORMANCE_NUTS = performance_nuts,
                _URI_LIST = uri_list,
                _VALUES = values,
            };

            return notice_data;
        }
        private CODIF_DATA ReadCodifData(XNamespace ns, XDocument xDocument, String parent)
        {
            String cd = "CODIF_DATA";

            DS_DATE_DISPATCH ds_date_dispatch = new DS_DATE_DISPATCH(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "DS_DATE_DISPATCH")!);
            DT_DATE_FOR_SUBMISSION ds_date_for_submission = new DT_DATE_FOR_SUBMISSION(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "DT_DATE_FOR_SUBMISSION")!);
            AA_AUTHORITY_TYPE aa_authority_type = new AA_AUTHORITY_TYPE(
                xDocument.Root.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "AA_AUTHORITY_TYPE")!);
            TD_DOCUMENT_TYPE td_document_type = new TD_DOCUMENT_TYPE(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "TD_DOCUMENT_TYPE")!);
            NC_CONTRACT_NATURE nc_contract_nature = new NC_CONTRACT_NATURE(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "NC_CONTRACT_NATURE")!);
            PR_PROC pr_doc = new PR_PROC(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "PR_PROC")!);
            RP_REGULATION rp_regulation = new RP_REGULATION(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "RP_REGULATION")!);
            TY_TYPE_BID ty_type_bid = new TY_TYPE_BID(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "TY_TYPE_BID")!);
            AC_AWARD_CRIT ac_award_crit = new AC_AWARD_CRIT(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "AC_AWARD_CRIT")!);
            MA_MAIN_ACTIVITIES ma_main_activities = new MA_MAIN_ACTIVITIES(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "MA_MAIN_ACTIVITIES")!);
            HEADING heading = new HEADING(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "HEADING")!);
            INITIATOR initiator = new INITIATOR(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "INITIATOR")!);
            DIRECTIVE directive_codif_data = new DIRECTIVE(
                xDocument.Root!.Element(ns + parent)!.Element(ns + cd)!.Element(ns + "DIRECTIVE")!);

            CODIF_DATA codif_data = new()
            {
                _DS_DATE_DISPATCH = ds_date_dispatch,
                _DT_DATE_FOR_SUBMISSION = ds_date_for_submission,
                _AA_AUTHORITY_TYPE = aa_authority_type,
                _TD_DOCUMENT_TYPE = td_document_type,
                _NC_CONTRACT_NATURE = nc_contract_nature,
                _PR_PROC = pr_doc,
                _RP_REGULATION = rp_regulation,
                _TY_TYPE_BID = ty_type_bid,
                _AC_AWARD_CRIT = ac_award_crit,
                _MA_MAIN_ACTIVITIES = ma_main_activities,
                _HEADING = heading,
                _INITIATOR = initiator,
                _DIRECTIVE = directive_codif_data,
            };

            return codif_data;
        }
        //3
        private CODED_DATA_SECTION ReadCodedDataSection(XNamespace ns, XDocument xDocument)
        {
            String cds = "CODED_DATA_SECTION";

            REF_OJS ref_ojs = new REF_OJS();
            try
            {
                ref_ojs = ReadRefOjs(ns, xDocument, cds);
            }catch(NullReferenceException) { }

            NOTICE_DATA notice_data = new NOTICE_DATA();
            try
            {
                notice_data = ReadNoticeData(ns, xDocument, cds);
            }
            catch (NullReferenceException) { }

            CODIF_DATA codif_data = new CODIF_DATA();
            try
            {
                codif_data = ReadCodifData(ns, xDocument, cds);
            }
            catch (NullReferenceException) { };

            //CODED_DATA_SECTION
            CODED_DATA_SECTION coded_data_section = new()
            {
                _REF_OJS = ref_ojs,
                _NOTICE_DATA = notice_data,
                _CODIF_DATA = codif_data
            };

            return coded_data_section;
        }

        //4.-
        private List<ML_TI_DOC> ReadTiDocList(XNamespace ns, XDocument xDocument, String parent1, String parent2)
        {
            String mti = "ML_TI_DOC";
            List<ML_TI_DOC> ti_doc_list = new List<ML_TI_DOC>();
            try
            {
                IEnumerable<XElement> ti_doc_root = xDocument.Root!.Element(ns + parent1)!.Element(ns + parent2)!.Elements(ns + mti)!;
                IEnumerable<XElement> ti_doc_ti_city = xDocument.Root!.Element(ns + parent1)!.Element(ns + parent2)!.Elements(ns + mti)!.Elements(ns + "TI_CY")!;
                IEnumerable<XElement> ti_doc_ti_town = xDocument.Root!.Element(ns + parent1)!.Element(ns + parent2)!.Elements(ns + mti)!.Elements(ns + "TI_TOWN")!;
                IEnumerable<XElement> ti_doc_ti_doc= xDocument.Root!.Element(ns + parent1)!.Element(ns + parent2)!.Elements(ns + mti)!.Elements(ns + "TI_TEXT")!;

                int count = ti_doc_root.Count();

                for (int i = 0; i<count; i++)
                {
                    ML_TI_DOC ti_doc = new ML_TI_DOC(ti_doc_root.ElementAt(i));

                    if(ti_doc._LG == "EN")
                    {
                        ti_doc._TI_CY = new TI_CY(ti_doc_ti_city.ElementAt(i));
                        ti_doc._TI_TOWN = new TI_TOWN(ti_doc_ti_town.ElementAt(i));
                        ti_doc._TI_TEXT = new TI_TEXT();
                        try
                        {
                            ti_doc._TI_TEXT._P = CreatePList(ti_doc_ti_doc.ElementAt(i).Elements(ns + "P")!);
                        }
                        catch (NullReferenceException) { }
                        ti_doc_list.Add(ti_doc);
                        break;
                    }
                }

            }
            catch (NullReferenceException) { }
            return ti_doc_list;
        }
        private TRANSLATION_SECTION ReadTranslationSection(XNamespace ns, XDocument xDocument)
        {
            String ts = "TRANSLATION_SECTION";
            String mts = "ML_TITLES";

            ML_TITLES ml_titles = new ML_TITLES();
            try
            {
                ml_titles._ML_TI_DOC = ReadTiDocList(ns, xDocument, ts, mts);
            }
            catch (NullReferenceException) { }

            String aan = "ML_AA_NAMES";

            ML_AA_NAMES ml_aa_names = new ML_AA_NAMES();
            try
            {
                ml_aa_names._AA_NAME = CreateAANameList(
                    xDocument.Root!.Element(ns + ts)!.Element(ns + aan)!.Elements(ns + "AA_NAME")!);
            } catch (NullReferenceException) { };

            //TRANSLATION_SECTION
            TRANSLATION_SECTION translation_section = new()
            {
                _ML_TITLES = ml_titles,
                _ML_AA_NAMES = ml_aa_names,
            };

            return translation_section;
        }


        //5.1.3
        private CONTRACTING_BODY ReadContractingBody(XNamespace ns, XDocument xDocument, String form_type, String parent)
        {
            String cb = "CONTRACTING_BODY";

            DOCUMENT_FULL document_full = new DOCUMENT_FULL(
                xDocument.Root!.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + "DOCUMENT_FULL")!);
            URL_DOCUMENT url_document = new URL_DOCUMENT(
                xDocument.Root!.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + "URL_DOCUMENT")!);
            URL_PARTICIPATION url_participation = new URL_PARTICIPATION(
                xDocument.Root!.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + "URL_PARTICIPATION")!);
            CA_TYPE ca_type = new CA_TYPE(
                xDocument.Root!.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + "CA_TYPE")!);
            CA_TYPE_OTHER ca_type_other_f12 = new CA_TYPE_OTHER(
                xDocument.Root!.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + "CA_TYPE_OTHER")!);
            CA_ACTIVITY ca_activity_f12 = new CA_ACTIVITY(
                xDocument.Root!.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + "CA_ACTIVITY")!);

            String acb = "ADDRESS_CONTRACTING_BODY";
            String afi = "ADDRESS_FURTHER_INFO";
            String ap = "ADDRESS_PARTICIPATION";
            ADDRESS_CONTRACTING_BODY addr_contracting_body = new ADDRESS_CONTRACTING_BODY();
            ADDRESS_FURTHER_INFO addr_futher_info = new ADDRESS_FURTHER_INFO();
            ADDRESS_PARTICIPATION addr_participation = new ADDRESS_PARTICIPATION();

            List<String> address_objects_list = new List<String>();
            if(xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + acb)! is not null) { address_objects_list.Add(acb); }
            if (xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + afi)! is not null) { address_objects_list.Add(afi); }
            if (xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + ap)! is not null) { address_objects_list.Add(ap); }

            foreach(string address_object in address_objects_list)
            {
                OFFICIALNAME officialname_acb = new OFFICIALNAME(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "OFFICIALNAME")!);
                NATIONALID nationalid_acb = new NATIONALID(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "NATIONALID")!);
                ADDRESS address_acb = new ADDRESS(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "ADDRESS")!);
                TOWN town_acb = new TOWN(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "TOWN")!);
                POSTAL_CODE postal_code_acb = new POSTAL_CODE(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "POSTAL_CODE")!);
                COUNTRY country_acb = new COUNTRY(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "COUNTRY")!);
                CONTACT_POINT contact_point_acb = new CONTACT_POINT(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "CONTACT_POINT")!);
                PHONE phone_acb = new PHONE(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "PHONE")!);
                E_MAIL e_mail_acb = new E_MAIL(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "E_MAIL")!);
                FAX fax_acb = new FAX(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "FAX")!);
                NUTS nuts_acb = new NUTS(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "NUTS")!);
                URL_GENERAL url_general_acb = new URL_GENERAL(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "URL_GENERAL")!);
                URL_BUYER url_buyer_acb = new URL_BUYER(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + cb)!.Element(ns + address_object)!.Element(ns + "URL_BUYER")!);
                
                if(address_object == acb)
                {
                    addr_contracting_body._OFFICIALNAME = officialname_acb;
                    addr_contracting_body._NATIONALID = nationalid_acb;
                    addr_contracting_body._ADDRESS = address_acb;
                    addr_contracting_body._TOWN = town_acb;
                    addr_contracting_body._POSTAL_CODE = postal_code_acb;
                    addr_contracting_body._COUNTRY = country_acb;
                    addr_contracting_body._CONTACT_POINT = contact_point_acb;
                    addr_contracting_body._PHONE = phone_acb;
                    addr_contracting_body._E_MAIL = e_mail_acb;
                    addr_contracting_body._FAX = fax_acb;
                    addr_contracting_body._NUTS = nuts_acb;
                    addr_contracting_body._URL_GENERAL = url_general_acb;
                    addr_contracting_body._URL_BUYER = url_buyer_acb;
                }
                if (address_object == afi)
                {
                    addr_futher_info._OFFICIALNAME = officialname_acb;
                    addr_futher_info._NATIONALID = nationalid_acb;
                    addr_futher_info._ADDRESS = address_acb;
                    addr_futher_info._TOWN = town_acb;
                    addr_futher_info._POSTAL_CODE = postal_code_acb;
                    addr_futher_info._COUNTRY = country_acb;
                    addr_futher_info._CONTACT_POINT = contact_point_acb;
                    addr_futher_info._PHONE = phone_acb;
                    addr_futher_info._E_MAIL = e_mail_acb;
                    addr_futher_info._FAX = fax_acb;
                    addr_futher_info._NUTS = nuts_acb;
                    addr_futher_info._URL_GENERAL = url_general_acb;
                    addr_futher_info._URL_BUYER = url_buyer_acb;
                }
                if (address_object == ap)
                {
                    addr_participation._OFFICIALNAME = officialname_acb;
                    addr_participation._NATIONALID = nationalid_acb;
                    addr_participation._ADDRESS = address_acb;
                    addr_participation._TOWN = town_acb;
                    addr_participation._POSTAL_CODE = postal_code_acb;
                    addr_participation._COUNTRY = country_acb;
                    addr_participation._CONTACT_POINT = contact_point_acb;
                    addr_participation._PHONE = phone_acb;
                    addr_participation._E_MAIL = e_mail_acb;
                    addr_participation._FAX = fax_acb;
                    addr_participation._NUTS = nuts_acb;
                    addr_participation._URL_GENERAL = url_general_acb;
                    addr_participation._URL_BUYER = url_buyer_acb;
                }
            }

            CONTRACTING_BODY contrating_body = new()
            {
                _DOCUMENT_FULL = document_full,
                _URL_DOCUMENT = url_document,
                _URL_PARTICIPATION = url_participation,
                _CA_TYPE = ca_type,
                _CA_TYPE_OTHER = ca_type_other_f12,
                _CA_ACTIVITY = ca_activity_f12,
                _ADDRESS_CONTRACTING_BODY = addr_contracting_body,
                _ADDRESS_FURTHER_INFO = addr_futher_info,
                _ADDRESS_PARTICIPATION = addr_participation,
            };

            return contrating_body;
        }
        private AC ReadAc(XNamespace ns, XDocument xDocument, String form_type, String parent1, String parent2, String parent3)
        {
            String acs = "AC";

            AC ac = new AC();

            AC_QUALITY ac_quality = new AC_QUALITY();
            AC_CRITERION ac_criterion_q = new AC_CRITERION();
            AC_WEIGHTING ac_weighting_q = new AC_WEIGHTING();
            if (xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + parent3)!.Element(ns + acs)!.Element(ns + "AC_QUALITY")! is not null)
            {
                ac_criterion_q._Text = xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + parent3)!.Element(ns + acs)!.Element(ns + "AC_QUALITY")!.Element(ns + "AC_CRITERION")!.Value.ToString();
                ac_weighting_q._Text = xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + parent3)!.Element(ns + acs)!.Element(ns + "AC_QUALITY")!.Element(ns + "AC_WEIGHTING")!.Value.ToString();
                ac_quality._AC_CRITERION = ac_criterion_q;
                ac_quality._AC_WEIGHTING = ac_weighting_q;
            }

            AC_PRICE ac_price = new AC_PRICE();
            AC_WEIGHTING ac_weighting_p = new AC_WEIGHTING();
            if (xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + parent3)!.Element(ns + acs)!.Element(ns + "AC_PRICE")! is not null)
            {
                try
                {
                    ac_weighting_p._Text = xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + parent3)!.Element(ns + acs)!.Element(ns + "AC_PRICE")!.Element(ns + "AC_WEIGHTING")!.Value.ToString();
                }
                catch (NullReferenceException) { }
                ac_price._AC_WEIGHTING = ac_weighting_p;
            }

            //AC
            ac._AC_QUALITY = ac_quality;
            ac._AC_PRICE = ac_price;
            
            return ac;
        }
        //5.1.4.-
        private OBJECT_DESCR ReadObjectDescr(XNamespace ns, XDocument xDocument, String form_type, String parent1, String parent2)
        {
            String od = "OBJECT_DESCR";

            List<CPV_ADDITIONAL> cpv_additional_list = CreateCpvAdditionalList(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Elements(ns + "CPV_ADDITIONAL")!, ns);

            NO_EU_PROGR_RELATED no_eu_progr_related = new NO_EU_PROGR_RELATED(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "NO_EU_PROGR_RELATED")!);
            NUTS nuts_od = new(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "NUTS")!);
            DURATION duration_od = new DURATION(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "DURATION")!);
            NO_RENEWAL no_renewal = new NO_RENEWAL(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "NO_RENEWAL")!);
            NB_ENVISAGED_CANDIDATE nb_envisaged_candidate = new NB_ENVISAGED_CANDIDATE(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "NB_ENVISAGED_CANDIDATE")!);
            NO_ACCEPTED_VARIANTS no_accepted_variants = new NO_ACCEPTED_VARIANTS(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "NO_ACCEPTED_VARIANTS")!);
            OPTIONS options_od = new OPTIONS(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "OPTIONS")!);
            ITEM item_od = new ITEM(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "ITEM")!);
            LOT_NO lot_not = new LOT_NO(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "LOT_NO")!);
            VAL_OBJECT val_object = new VAL_OBJECT(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "VAL_OBJECT")!);
            RENEWAL renewal_od = new RENEWAL(
                xDocument.Root!.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "RENEWAL")!);

            SHORT_DESCR short_descr_od = new SHORT_DESCR();
            try
            {
                short_descr_od._P = CreatePList(
                    xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "SHORT_DESCR")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            MAIN_SITE main_site = new MAIN_SITE();
            try
            {
                main_site._P = CreatePList(
                    xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "MAIN_SITE")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            CRITERIA_CANDIDATE criteria_candidate = new CRITERIA_CANDIDATE();
            try
            {
                criteria_candidate._P = CreatePList(
                    xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "CRITERIA_CANDIDATE")!.Elements(ns + "P"));
            }
            catch (NullReferenceException) { }

            OPTIONS_DESCR options_descr_od = new OPTIONS_DESCR();
            try
            {
                options_descr_od._P = CreatePList(
                    xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "OPTIONS_DESCR")!.Elements(ns + "P"));
            }
            catch (NullReferenceException) { }

            TITLE title_od = new TITLE();
            try
            {
                title_od._P = CreatePList(
                    xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "TITLE")!.Elements(ns + "P"));
            }
            catch (NullReferenceException) { }

            RENEWAL_DESCR renewal_descr_od = new RENEWAL_DESCR();
            try
            {
                renewal_descr_od._P = CreatePList(
                    xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "RENEWAL_DESCR")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            INFO_ADD info_add_od = new INFO_ADD();
            try
            {
                info_add_od._P = CreatePList(
                    xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "INFO_ADD")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            //externalizado a metodo
            AC ac = new AC();
            if (xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + od)!.Element(ns + "AC")! is not null)
            {
                ac = ReadAc(ns, xDocument, form_type, parent1, parent2, od);
            }

            OBJECT_DESCR object_descr = new()
            {
                _CPV_ADDITIONAL = cpv_additional_list,
                _SHORT_DESCR = short_descr_od,
                _NO_EU_PROGR_RELATED = no_eu_progr_related,
                _MAIN_SITE = main_site,
                _NUTS = nuts_od,
                _AC = ac,
                _DURATION = duration_od,
                _NO_RENEWAL = no_renewal,
                _NB_ENVISAGED_CANDIDATE = nb_envisaged_candidate,
                _CRITERIA_CANDIDATE = criteria_candidate,
                _NO_ACCEPTED_VARIANTS = no_accepted_variants,
                _OPTIONS = options_od,
                _OPTIONS_DESCR = options_descr_od,
                _ITEM = item_od,
                _TITLE = title_od,
                _LOT_NO = lot_not,
                _VAL_OBJECT = val_object,
                _RENEWAL = renewal_od,
                _RENEWAL_DESCR = renewal_descr_od,
                _INFO_ADD = info_add_od,
            };

            return object_descr;
        }
        //5.1.4
        private OBJECT_CONTRACT ReadObjectContract(XNamespace ns, XDocument xDocument, String form_type, String parent)
        {
            String ob = "OBJECT_CONTRACT";

            REFERENCE_NUMBER reference_number = new REFERENCE_NUMBER(
                 xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ob)!.Element(ns + "REFERENCE_NUMBER")!);
            TYPE_CONTRACT type_contract = new TYPE_CONTRACT(
                xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ob)!.Element(ns + "TYPE_CONTRACT")!);
            NO_LOT_DIVISION no_lot_division = new NO_LOT_DIVISION(
                xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ob)!.Element(ns + "NO_LOT_DIVISION")!);
            VAL_ESTIMATED_TOTAL val_estimated_total = new VAL_ESTIMATED_TOTAL(
                xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ob)!.Element(ns + "VAL_ESTIMATED_TOTAL")!);

            CPV_MAIN cpv_main = new CPV_MAIN();
            try
            {
                cpv_main._CPV_CODE = new CPV_CODE(
                     xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ob)!.Element(ns + "CPV_MAIN")!.Element(ns + "CPV_CODE")!);
            } catch (NullReferenceException) { };

            TITLE title = new TITLE();
            try
            {
                title._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ob)!.Element(ns + "TITLE")!.Elements(ns + "P")!);  
            }
            catch (NullReferenceException) { }

            SHORT_DESCR short_descr_oc = new SHORT_DESCR();
            try
            {
                short_descr_oc._P = CreatePList(
                   xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ob)!.Element(ns + "SHORT_DESCR")!.Elements(ns + "P")!);
            }
            catch(NullReferenceException) { }

            LOT_DIVISION lot_division = new LOT_DIVISION();
            try
            {
                lot_division._LOT_ALL = new LOT_ALL(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ob)!.Element(ns + "LOT_DIVISION")!.Element(ns + "LOT_ALL")!);
            }
            catch (NullReferenceException) { }

            //extracted into function
            OBJECT_DESCR object_descr = new OBJECT_DESCR();
            if (xDocument.Root!.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ob)!.Element(ns + "OBJECT_DESCR") is not null)
            {
                object_descr = ReadObjectDescr(ns, xDocument, form_type, parent, ob);
            }

            OBJECT_CONTRACT object_contract = new()
            {
                _TITLE = title,
                _REFERENCE_NUMBER = reference_number,
                _CPV_MAIN = cpv_main,
                _OBJECT_DESCR = object_descr,
                _TYPE_CONTRACT = type_contract,
                _SHORT_DESCR = short_descr_oc,
                _NO_LOT_DIVISION = no_lot_division,
                _VAL_ESTIMATED_TOTAL = val_estimated_total,
                _LOT_DIVISION = lot_division,
            };

            return object_contract;
        }
        //5.1.5
        private LEFTI ReadLefti(XNamespace ns, XDocument xDocument, String form_type, String parent)
        {
            String le = "LEFTI";

            PARTICULAR_PROFESSION particular_profession = new PARTICULAR_PROFESSION(
                xDocument.Root!.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + le)!.Element(ns + "PARTICULAR_PROFESSION")!);
            try
            {
                particular_profession._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + le)!.Element(ns + "PARTICULAR_PROFESSION")!.Elements(ns + "P")!); 
            }
            catch (NullReferenceException) { };

            CRITERIA_SELECTION criteria_selection = new CRITERIA_SELECTION();
            try
            {
                criteria_selection._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + le)!.Element(ns + "CRITERIA_SELECTION")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            SUITABILITY suitability = new SUITABILITY();
            try
            {
                suitability._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + le)!.Element(ns + "SUITABILITY")!.Elements(ns + "P")!);
            }
            catch(NullReferenceException) { }

            ECONOMIC_FINANCIAL_INFO economic_financial_info = new ECONOMIC_FINANCIAL_INFO();
            try
            {
                economic_financial_info._P = CreatePList(
                   xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + le)!.Element(ns + "ECONOMIC_FINANCIAL_INFO")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            TECHNICAL_PROFESSIONAL_INFO technical_professional_info = new TECHNICAL_PROFESSIONAL_INFO();
            try
            {
                technical_professional_info._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + le)!.Element(ns + "TECHNICAL_PROFESSIONAL_INFO")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            REFERENCE_TO_LAW reference_to_law = new REFERENCE_TO_LAW();
            try
            {
                reference_to_law._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + le)!.Element(ns + "REFERENCE_TO_LAW")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            PERFORMANCE_CONDITIONS performance_conditions = new PERFORMANCE_CONDITIONS();
            try
            {
                performance_conditions._P = CreatePList(
                   xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + le)!.Element(ns + "PERFORMANCE_CONDITIONS")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            LEFTI lefti = new LEFTI()
            {
                _CRITERIA_SELECTION = criteria_selection,
                _PARTICULAR_PROFESSION = particular_profession,
                _SUITABILITY = suitability,
                _ECONOMIC_FINANCIAL_INFO = economic_financial_info,
                _TECHNICAL_PROFESSIONAL_INFO = technical_professional_info,
                _REFERENCE_TO_LAW = reference_to_law,
                _PERFORMANCE_CONDITIONS = performance_conditions,
            };

            return lefti;
        }
        //5.1.6.-
        private OPENING_CONDITION ReadOpeningCondition(XNamespace ns, XDocument xDocument, String form_type, String parent1, String parent2)
        {
            String op = "OPENING_CONDITION";

            DATE_OPENING_TENDERS date_opening_tenders = new DATE_OPENING_TENDERS(
                xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + op)!.Element(ns + "DATE_OPENING_TENDERS")!);
            TIME_OPENING_TENDERS time_opening_tenders = new TIME_OPENING_TENDERS(
                xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + op)!.Element(ns + "TIME_OPENING_TENDERS")!);

            PLACE place = new PLACE() { };
            try
            {
                place._P = CreatePList(
                    xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + op)!.Element(ns + "PLACE")!.Elements(ns + "P")!);

            }
            catch (NullReferenceException) { };

            INFO_ADD info_add_op = new INFO_ADD() { };
            try
            {
                info_add_op._P = CreatePList(
                    xDocument.Root.Element(ns + parent1)!.Element(ns + form_type)!.Element(ns + parent2)!.Element(ns + op)!.Element(ns + "INFO_ADD")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { };

            OPENING_CONDITION opening_condition = new OPENING_CONDITION()
            {
                _DATE_OPENING_TENDERS = date_opening_tenders,
                _TIME_OPENING_TENDERS = time_opening_tenders,
                _PLACE = place,
                _INFO_ADD = info_add_op
            };

            return opening_condition;
        }
        //5.1.6
        private PROCEDURE ReadProcedure(XNamespace ns, XDocument xDocument, String procedure_type, String form_type, String parent)
        {
            //falta el tipo de procedimiento
            String pr = "PROCEDURE";

            NB_PARTICIPANTS nb_participants = new NB_PARTICIPANTS(
                xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + pr)!.Element(ns + "NB_PARTICIPANTS")!);
            NB_MIN_PARTICIPANTS nb_min_participants = new NB_MIN_PARTICIPANTS(
                xDocument.Root.Element(ns + parent)!.Element(ns +  form_type)!.Element(ns + pr)!.Element(ns + "NB_MIN_PARTICIPANTS")!);            
            NB_MAX_PARTICIPANTS nb_max_participants = new NB_MAX_PARTICIPANTS(
                 xDocument.Root.Element(ns + parent)!.Element(ns +  form_type)!.Element(ns + pr)!.Element(ns + "NB_MAX_PARTICIPANTS")!);            
            DATE_RECEIPT_TENDERS date_receipt_tenders = new DATE_RECEIPT_TENDERS(
                 xDocument.Root.Element(ns + parent)!.Element(ns +  form_type)!.Element(ns + pr)!.Element(ns + "DATE_RECEIPT_TENDERS")!);            
            TIME_RECEIPT_TENDERS time_receipt_tenders = new TIME_RECEIPT_TENDERS(
                 xDocument.Root.Element(ns + parent)!.Element(ns +  form_type)!.Element(ns + pr)!.Element(ns + "TIME_RECEIPT_TENDERS")!);
            PRIZE_AWARDED prize_awarded = new PRIZE_AWARDED(
                xDocument.Root.Element(ns + parent)!.Element(ns +  form_type)!.Element(ns + pr)!.Element(ns + "PRIZE_AWARDED")!);
            FOLLOW_UP_CONTRACTS follow_up_contracts = new FOLLOW_UP_CONTRACTS(
                xDocument.Root.Element(ns + parent)!.Element(ns +  form_type)!.Element(ns + pr)!.Element(ns + "FOLLOW_UP_CONTRACTS")!);            
            NO_DECISION_BINDING_CONTRACTING no_decision_binding_contracting = new NO_DECISION_BINDING_CONTRACTING(
                xDocument.Root.Element(ns + parent)!.Element(ns +  form_type)!.Element(ns + pr)!.Element(ns + "NO_DECISION_BINDING_CONTRACTING")!);            
            CONTRACT_COVERED_GPA contract_covered_gpa = new CONTRACT_COVERED_GPA(
                xDocument.Root.Element(ns + parent)!.Element(ns +  form_type)!.Element(ns + pr)!.Element(ns + "CONTRACT_COVERED_GPA")!);            
            DURATION_TENDER_VALID duration_tender_vali = new DURATION_TENDER_VALID(
                xDocument.Root.Element(ns + parent)!.Element(ns +  form_type)!.Element(ns + pr)!.Element(ns + "DURATION_TENDER_VALID")!);

            CRITERIA_EVALUATION criteria_evaluation = new CRITERIA_EVALUATION() {};
            try
            {
                criteria_evaluation._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + pr)!.Element(ns + "CRITERIA_EVALUATION")!.Elements(ns + "P")!);
            }catch(NullReferenceException) { }
            
            LANGUAGES languages = new LANGUAGES() { };
            try
            {
                languages._LANGUAGE = CreateLanguageList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + pr)!.Element(ns + "LANGUAGES")!.Elements(ns + "LANGUAGE")!);
            }
            catch(NullReferenceException) { }

            NUMBER_VALUE_PRIZE number_value_prize = new() { };
            try
            {
                number_value_prize._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + pr)!.Element(ns + "NUMBER_VALUE_PRIZE")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            FRAMEWORK framework = new FRAMEWORK() { };
            try
            {
                framework._SINGLE_OPERATOR = CreateSingleOperatorList(
                   xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + pr)!.Element(ns + "FRAMEWORK")!.Elements(ns + "SINGLE_OPERATOR")!);
            }
            catch (NullReferenceException) { }

            OPENING_CONDITION opening_condition = new OPENING_CONDITION();
            try
            {
                opening_condition = ReadOpeningCondition(ns, xDocument, form_type, parent, pr);
            }
            catch (NullReferenceException) { }

            PROCEDURE procedure = new PROCEDURE()
            {
                _first_element = procedure_type,
                _NB_PARTICIPANTS = nb_participants,
                _NB_MIN_PARTICIPANTS = nb_min_participants,
                _NB_MAX_PARTICIPANTS = nb_max_participants,
                _CRITERIA_EVALUATION = criteria_evaluation,
                _DATE_RECEIPT_TENDERS = date_receipt_tenders,
                _TIME_RECEIPT_TENDERS = time_receipt_tenders,
                _LANGUAGES = languages,
                _PRIZE_AWARDED = prize_awarded,
                _NUMBER_VALUE_PRIZE = number_value_prize,
                _FOLLOW_UP_CONTRACTS = follow_up_contracts,
                _NO_DECISION_BINDING_CONTRACTING = no_decision_binding_contracting,
                _CONTRACT_COVERED_GPA = contract_covered_gpa,
                _DURATION_TENDER_VALID = duration_tender_vali,
                _FRAMEWORK = framework,
                _OPENING_CONDITION = opening_condition,
            };
            return procedure;
        }
        //5.1.7
        private COMPLEMENTARY_INFO ReadComplementaryInfo(XNamespace ns, XDocument xDocument, String form_type, String parent)
        {
            #region complementary_info
            String ci = "COMPLEMENTARY_INFO";

            DATE_DISPATCH_NOTICE date_dispatch_notice = new DATE_DISPATCH_NOTICE(
                xDocument.Root!.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + "DATE_DISPATCH_NOTICE")!);

            REVIEW_PROCEDURE review_procedure = new() { };
            try
            {
                review_procedure._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + "REVIEW_PROCEDURE")!.Elements(ns + "P")!);
            }
            catch (NullReferenceException) { }

            INFO_ADD info_add_ci = new() { };
            try
            {
                info_add_ci._P = CreatePList(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + "INFO_ADD")!.Elements(ns + "P")!);
            }
            catch(NullReferenceException) { }

            String arb = "ADDRESS_REVIEW_BODY";
            String amb = "ADDRESS_MEDIATION_BODY";
            String ari = "ADDRESS_REVIEW_INFO";
            List<String> address_objects_list = new List<String>();
            if(xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + arb) is not null) { address_objects_list.Add(arb); }
            if(xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + amb) is not null) { address_objects_list.Add(amb); }
            if(xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + ari) is not null) { address_objects_list.Add(ari); }

            ADDRESS_REVIEW_BODY address_review_body = new ADDRESS_REVIEW_BODY();
            ADDRESS_MEDIATION_BODY address_mediation_body = new ADDRESS_MEDIATION_BODY();
            ADDRESS_REVIEW_INFO address_review_info = new ADDRESS_REVIEW_INFO();

            foreach (String element in address_objects_list)
            {
                #region subclasses
                OFFICIALNAME officialname_arb = new OFFICIALNAME(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "OFFICIALNAME")!);
                ADDRESS address_arb = new ADDRESS(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "ADDRESS")!);
                TOWN town_arb = new TOWN(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "TOWN")!);
                POSTAL_CODE postal_code_arb = new POSTAL_CODE(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "POSTAL_CODE")!);
                COUNTRY country_arb = new COUNTRY(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "COUNTRY")!);
                CONTACT_POINT contact_point_arb = new CONTACT_POINT(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "CONTACT_POINT")!);
                PHONE phone_arb = new PHONE(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "PHONE")!);
                E_MAIL e_mail_arb = new E_MAIL(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "E_MAIL")!);
                FAX fax_arb = new FAX(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "FAX")!);
                NUTS nuts_arb = new NUTS(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "NUTS")!);
                URL_GENERAL url_general_arb = new URL_GENERAL(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "URL_GENERAL")!);
                URL_BUYER url_buyer_arb = new URL_BUYER(
                    xDocument.Root.Element(ns + parent)!.Element(ns + form_type)!.Element(ns + ci)!.Element(ns + element)!.Element(ns + "URL_BUYER")!);
                #endregion subclasses

                if (element == arb)
                {
                    address_review_body._OFFICIALNAME = officialname_arb;
                    address_review_body._ADDRESS = address_arb;
                    address_review_body._TOWN = town_arb;
                    address_review_body._POSTAL_CODE = postal_code_arb;
                    address_review_body._COUNTRY = country_arb;
                    address_review_body._CONTACT_POINT = contact_point_arb;
                    address_review_body._PHONE = phone_arb;
                    address_review_body._E_MAIL = e_mail_arb;
                    address_review_body._FAX = fax_arb;
                    address_review_body._NUTS = nuts_arb;
                    address_review_body._URL_GENERAL = url_general_arb;
                    address_review_body._URL_BUYER = url_buyer_arb;
                }

                if (element == amb)
                {
                    address_mediation_body._OFFICIALNAME = officialname_arb;
                    address_mediation_body._ADDRESS = address_arb;
                    address_mediation_body._TOWN = town_arb;
                    address_mediation_body._POSTAL_CODE = postal_code_arb;
                    address_mediation_body._COUNTRY = country_arb;
                    address_mediation_body._CONTACT_POINT = contact_point_arb;
                    address_mediation_body._PHONE = phone_arb;
                    address_mediation_body._E_MAIL = e_mail_arb;
                    address_mediation_body._FAX = fax_arb;
                    address_mediation_body._NUTS = nuts_arb;
                    address_mediation_body._URL_GENERAL = url_general_arb;
                    address_mediation_body._URL_BUYER = url_buyer_arb;
                }

                if (element == ari)
                {
                    address_review_info._OFFICIALNAME = officialname_arb;
                    address_review_info._ADDRESS = address_arb;
                    address_review_info._TOWN = town_arb;
                    address_review_info._POSTAL_CODE = postal_code_arb;
                    address_review_info._COUNTRY = country_arb;
                    address_review_info._CONTACT_POINT = contact_point_arb;
                    address_review_info._PHONE = phone_arb;
                    address_review_info._E_MAIL = e_mail_arb;
                    address_review_info._FAX = fax_arb;
                    address_review_info._NUTS = nuts_arb;
                    address_review_info._URL_GENERAL = url_general_arb;
                    address_review_info._URL_BUYER = url_buyer_arb;
                }
            }

            #endregion complementary_info
            COMPLEMENTARY_INFO complementary_info = new COMPLEMENTARY_INFO()
            {
                _INFO_ADD = info_add_ci,
                _ADDRESS_REVIEW_BODY = address_review_body,
                _ADDRESS_MEDIATION_BODY = address_mediation_body,
                _ADDRESS_REVIEW_INFO = address_review_info,
                _REVIEW_PROCEDURE = review_procedure,
                _DATE_DISPATCH_NOTICE = date_dispatch_notice,
            };

            return complementary_info;
        }
        //5
        private FORM_SECTION ReadFormSection(XNamespace ns, XDocument xDocument, XmlNode node, String form_type)
        {
            String fs = "FORM_SECTION";

            XmlNodeList forms_children = node.ChildNodes;

            LEGAL_BASIS legal_basis = new LEGAL_BASIS();
            DIRECTIVE directive_form = new DIRECTIVE();
            CONTRACTING_BODY contracting_body = new CONTRACTING_BODY();
            OBJECT_CONTRACT object_contract = new OBJECT_CONTRACT();
            LEFTI lefti = new LEFTI();
            PROCEDURE procedure = new PROCEDURE();
            COMPLEMENTARY_INFO complementary_info = new COMPLEMENTARY_INFO();

            foreach (XmlNode forms_child in forms_children)
            {
                try
                {
                    switch (forms_child.Name)
                    {
                        case "LEGAL_BASIS":
                            //5.1.1
                            legal_basis = new LEGAL_BASIS(xDocument.Root!.Element(ns + fs)!.Element(ns + form_type)!.Element(ns + "LEGAL_BASIS")!);
                            continue;
                        case "DIRECTIVE":
                            //5.1.2
                            directive_form = new DIRECTIVE(xDocument.Root!.Element(ns + fs)!.Element(ns + form_type)!.Element(ns + "DIRECTIVE")!);
                            continue;
                        case "CONTRACTING_BODY":
                            contracting_body = ReadContractingBody(ns, xDocument, form_type, fs);
                            continue;
                        case "OBJECT_CONTRACT":
                            //5.1.4
                            object_contract = ReadObjectContract(ns, xDocument, form_type, fs);
                            continue;
                        case "LEFTI":
                            //5.1.5
                            lefti = ReadLefti(ns, xDocument, form_type, fs);
                            continue;
                        case "PROCEDURE":
                            //5.1.6
                            //extraer procedure_type
                            XmlNodeList procedure_children = forms_child.ChildNodes;
                            String procedure_type = procedure_children[0]!.Name;

                            procedure = ReadProcedure(ns, xDocument, procedure_type, form_type, fs);
                            continue;
                        case "COMPLEMENTARY_INFO":
                            //5.1.7
                            complementary_info = ReadComplementaryInfo(ns, xDocument, form_type, fs);
                            continue;
                    }
                }
                catch (ArgumentNullException) { }
            }

            FORM form = new FORM();
            try
            {
                form = new FORM()
                {
                    _CATEGORY = xDocument.Root.Element(ns + fs)!.Element(ns + form_type)!.Attribute("CATEGORY")!.Value.ToString(),
                    _FORM = xDocument.Root.Element(ns + fs)!.Element(ns + form_type)!.Attribute("FORM")!.Value.ToString(),
                    _LG = xDocument.Root.Element(ns + fs)!.Element(ns + form_type)!.Attribute("LG")!.Value.ToString(),
                    _LEGAL_BASIS = legal_basis,
                    _DIRECTIVE = directive_form,
                    _CONTRACTING_BODY = contracting_body,
                    _OBJECT_CONTRACT = object_contract,
                    _LEFTI = lefti,
                    _PROCEDURE = procedure,
                    _COMPLEMENTARY_INFO = complementary_info,
                };
            }catch(Exception) { }

            //FORMS_SECTION
            FORM_SECTION form_section = new()
            {
                _FORM = form,
            };

            return form_section;
        }

    }
}

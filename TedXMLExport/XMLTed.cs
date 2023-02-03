using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml.Serialization;

/*
 * OK  F12 restricted cn-design
 * OK  F12 open cn-design
 * OK  F02 restricted cn-standard
 * F02- 4-or-6 cn-standard -> utilities
 * OK F02 comp-dial cn-standard
 * F02 neg-w-call cn-standard
 */
namespace LeerTedXML
{
    [XmlRoot(ElementName = "TED_EXPORT")]
    public class TED_EXPORT
    {
        //ATTRIBUTES
        [XmlAttribute(AttributeName = "xmlns")]
        public string? Xmlns { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "xsi")]
        public string? Xsi { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "xlink")]
        public string? Xlink { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "VERSION")]
        public string? VERSION { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "schemaLocation")]
        public string? SchemaLocation { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "DOC_ID")]
        public string? DOC_ID { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "EDITION")]
        public string? EDITION { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "n2021")]
        public string? N2021 { get; set; } = string.Empty;

        //XML ELEMENTS
        [XmlElement(ElementName = "TECHNICAL_SECTION")]
        public TECHNICAL_SECTION? TECHNICAL_SECTION { get; set; } = new TECHNICAL_SECTION();

        [XmlElement(ElementName = "LINKS_SECTION")]
        public LINKS_SECTION? LINKS_SECTION { get; set; } = new LINKS_SECTION();

        [XmlElement(ElementName = "CODED_DATA_SECTION")] 
        public CODED_DATA_SECTION? CODED_DATA_SECTION { get; set; } = new CODED_DATA_SECTION();

        [XmlElement(ElementName = "TRANSLATION_SECTION")]
        public TRANSLATION_SECTION? TRANSLATION_SECTION { get; set; } = new TRANSLATION_SECTION();

        [XmlElement(ElementName = "FORM_SECTION")]
        public FORM_SECTION? FORM_SECTION { get; set; } = new FORM_SECTION();

    }

    /// <summary>
    /// [1] TECHNICAL_SECTION 
    /// [1] LINKS_SECTION
    /// [1] CODED_DATA_SECTION
    /// [1] TRANSLATION_SECTION
    /// [1] FORM_SECTION
    /// </summary>
    #region SECCIONES PRINCIPALES
    [XmlRoot(ElementName = "TECHNICAL_SECTION")]
    public class TECHNICAL_SECTION
    {
        //ELEMENTS
        [XmlElement(ElementName = "RECEPTION_ID")]
        public RECEPTION_ID? _RECEPTION_ID { get; set; } = new RECEPTION_ID();

        [XmlElement(ElementName = "DELETION_DATE")]
        public DELETION_DATE? _DELETION_DATE { get; set; } = new DELETION_DATE();

        [XmlElement(ElementName = "FORM_LG_LIST")]
        public FORM_LG_LIST? _FORM_LG_LIST { get; set; } = new FORM_LG_LIST();

        [XmlElement(ElementName = "COMMENTS")]
        public COMMENTS? _COMMENTS { get; set; } = new COMMENTS();

    }

    [XmlRoot(ElementName = "LINKS_SECTION")]
    public class LINKS_SECTION
    {
        //ELEMENTS
        [XmlElement(ElementName = "XML_SCHEMA_DEFINITION_LINK")]
        public XML_SCHEMA_DEFINITION_LINK? _XML_SCHEMA_DEFINITION_LINK { get; set; } = new XML_SCHEMA_DEFINITION_LINK();

        [XmlElement(ElementName = "OFFICIAL_FORMS_LINK")]
        public OFFICIAL_FORMS_LINK? _OFFICIAL_FORMS_LINK { get; set; } = new OFFICIAL_FORMS_LINK();

        [XmlElement(ElementName = "FORMS_LABELS_LINK")]
        public FORMS_LABELS_LINK? _FORMS_LABELS_LINK { get; set; } = new FORMS_LABELS_LINK();

        [XmlElement(ElementName = "ORIGINAL_CPV_LINK")]
        public ORIGINAL_CPV_LINK? _ORIGINAL_CPV_LINK { get; set; } = new ORIGINAL_CPV_LINK();

        [XmlElement(ElementName = "ORIGINAL_NUTS_LINK")]
        public ORIGINAL_NUTS_LINK? _ORIGINAL_NUTS_LINK { get; set; } = new ORIGINAL_NUTS_LINK();
    }

    [XmlRoot(ElementName = "CODED_DATA_SECTION")]
    public class CODED_DATA_SECTION
    {
        //ELEMENTS
        [XmlElement(ElementName = "REF_OJS")]
        public REF_OJS? _REF_OJS { get; set; } = new REF_OJS();

        [XmlElement(ElementName = "NOTICE_DATA")]
        public NOTICE_DATA? _NOTICE_DATA { get; set; } = new NOTICE_DATA();

        [XmlElement(ElementName = "CODIF_DATA")]
        public CODIF_DATA? _CODIF_DATA { get; set; } = new CODIF_DATA();
    }

    [XmlRoot(ElementName = "TRANSLATION_SECTION")]
    public class TRANSLATION_SECTION
    {
        [XmlElement(ElementName = "ML_TITLES")]
        public ML_TITLES? _ML_TITLES { get; set; } = new ML_TITLES();

        [XmlElement(ElementName = "ML_AA_NAMES")]
        public ML_AA_NAMES? _ML_AA_NAMES { get; set; } = new ML_AA_NAMES();
    }

    [XmlRoot(ElementName = "FORM_SECTION")]
    public class FORM_SECTION
    {
        //ELEMENTS
        public FORM? _FORM { get; set; } = new FORM();

    }
    #endregion SECCIONES PRINCIPALES



    /// <summary>
    /// [1] TECHNICAL_SECTION
    ///     [2] RECEPTION_ID
    ///     [2] DELETION_DATE
    ///     [2] FROM_LG_LIST
    ///     [2] COMMENTS
    /// </summary>
    #region [1] TECHNICAL SECTION
    [XmlRoot(ElementName = "RECEPTION_ID")]
    public class RECEPTION_ID
    {
        //attr
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public RECEPTION_ID()
        {

        }
        public RECEPTION_ID(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }


    }

    [XmlRoot(ElementName = "DELETION_DATE")]
    public class DELETION_DATE
    {
        //attr
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public DELETION_DATE()
        {

        }
        public DELETION_DATE(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            } catch(NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }

    }

    [XmlRoot(ElementName = "FORM_LG_LIST")]
    public class FORM_LG_LIST
    {
        //attr
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public FORM_LG_LIST()
        {

        }
        public FORM_LG_LIST(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }

    }

    [XmlRoot(ElementName = "COMMENTS")]
    public class COMMENTS
    {
        //attr
        public XElement? _root { get; set; }
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public COMMENTS()
        {

        }
        public COMMENTS(XElement root)
        {
            try
            {
                this._root= root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }

        }

    }
    #endregion [1] TECHNICAL SECTION


    /// <summary>
    /// [1] LINKS_SECTION
    ///  *  [2] XML_SCHEMA_DEFINITION_LINK
    ///  *  [2] OFFICIAL_FORMS_LINK
    ///  *  [2] FORMS_LABELS_LINK
    ///  *  [2] ORIGINAL_CPV_LINK
    ///  *  [2] ORIGINAL_NUTS_LINK
    /// </summary>
    #region [1] LINKS_SECTION
    [XmlRoot(ElementName = "XML_SCHEMA_DEFINITION_LINK")]
    public class XML_SCHEMA_DEFINITION_LINK
    {
        //ATTRIBUTES
        [XmlAttribute(AttributeName = "title")]
        public string? _Title { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "href")]
        public string? _Href { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "type")]
        public string? _Type { get; set; } = string.Empty;
    }

    [XmlRoot(ElementName = "OFFICIAL_FORMS_LINK")]
    public class OFFICIAL_FORMS_LINK
    {
        //ATTRIBUTES
        [XmlAttribute(AttributeName = "href")]
        public string? _Href { get; set; } = string.Empty;
        [XmlAttribute(AttributeName = "type")]
        public string? _Type { get; set; } = string.Empty;
    }

    [XmlRoot(ElementName = "FORMS_LABELS_LINK")]
    public class FORMS_LABELS_LINK
    {
        //ATTRIBUTES
        [XmlAttribute(AttributeName = "href")]
        public string? _Href { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "type")]
        public string? _Type { get; set; } = string.Empty;
    }

    [XmlRoot(ElementName = "ORIGINAL_CPV_LINK")]
    public class ORIGINAL_CPV_LINK
    {
        //ATTRIBUTES
        [XmlAttribute(AttributeName = "href")]
        public string? _Href { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "type")]
        public string? _Type { get; set; } = string.Empty;
    }

    [XmlRoot(ElementName = "ORIGINAL_NUTS_LINK")]
    public class ORIGINAL_NUTS_LINK
    {
        //ATTRIBUTES
        [XmlAttribute(AttributeName = "href")]
        public string? _Href { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "type")]
        public string? _Type { get; set; } = string.Empty;
    }
    #endregion [1] LINKS_SECTION


    /// <summary>
    ///*[1] CODED_DATA_SECTION
    ///     [2] REF_OJS
    ///     [2] NOTICE_DATA
    ///     [2] CODIF_DATA
    /// </summary>
    #region [1] CODED_DATA_SECTION
    [XmlRoot(ElementName = "REF_OJS")]
    public class REF_OJS
    {
        //ELEMENTS
        [XmlElement(ElementName = "COLL_OJ")]
        public COLL_OJ? _COLL_OJ { get; set; } = new COLL_OJ();

        [XmlElement(ElementName = "NO_OJ")]
        public NO_OJ? _NO_OJ { get; set; } = new NO_OJ();

        [XmlElement(ElementName = "DATE_PUB")]
        public DATE_PUB? _DATE_PUB { get; set; } = new DATE_PUB();
    }

    #region [1] CODED_DATA_SECTION [2] REF_OJS
    [XmlRoot(ElementName = "COLL_OJ")]
    public class COLL_OJ
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public COLL_OJ()
        {

        }
        public COLL_OJ(XElement root)
        {   
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NO_OJ")]
    public class NO_OJ
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; }

        //ctor
        public NO_OJ()
        {

        }
        public NO_OJ(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "DATE_PUB")]
    public class DATE_PUB
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; }

        //ctor
        public DATE_PUB()
        {

        }
        public DATE_PUB(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }
    #endregion [1] CODED_DATA_SECTION [2] REF_OJS

    [XmlRoot(ElementName = "NOTICE_DATA")]
    public class NOTICE_DATA
    {
        //ELEMENTS
        [XmlElement(ElementName = "NO_DOC_OJS")]
        public NO_DOC_OJS? _NO_DOC_OJS { get; set; } = new NO_DOC_OJS();

        [XmlElement(ElementName = "URI_LIST")]
        public URI_LIST? _URI_LIST { get; set; } = new URI_LIST();

        [XmlElement(ElementName = "LG_ORIG")]
        public LG_ORIG? _LG_ORIG { get; set; } = new LG_ORIG();

        [XmlElement(ElementName = "ISO_COUNTRY")]
        public ISO_COUNTRY? _ISO_COUNTRY { get; set; } = new ISO_COUNTRY();

        [XmlElement(ElementName = "IA_URL_GENERAL")]
        public IA_URL_GENERAL? _IA_URL_GENERAL { get; set; } = new IA_URL_GENERAL();

        [XmlElement(ElementName = "IA_URL_ETENDERING")]
        public IA_URL_ETENDERING? _IA_URL_ETENDERING { get; set; } = new IA_URL_ETENDERING();

        [XmlElement(ElementName = "ORIGINAL_CPV")]
        public List<ORIGINAL_CPV>? _ORIGINAL_CPV { get; set; } = new List<ORIGINAL_CPV> { };

        [XmlElement(ElementName = "ORIGINAL_NUTS")]
        public ORIGINAL_NUTS? _ORIGINAL_NUTS { get; set; } = new ORIGINAL_NUTS();

        [XmlElement(ElementName = "CA_CE_NUTS")]
        public CA_CE_NUTS? _CA_CE_NUTS { get; set; } = new CA_CE_NUTS();

        [XmlElement(ElementName = "PERFORMANCE_NUTS")]
        public PERFORMANCE_NUTS? _PERFORMANCE_NUTS { get; set; } = new PERFORMANCE_NUTS();

        [XmlElement(ElementName = "VALUES")]
        public VALUES? _VALUES { get; set; } = new VALUES();
    }

    #region [1] CODED_DATA_SECTION [2] NOTICE_DATA
    [XmlRoot(ElementName = "NO_DOC_OJS")]
    public class NO_DOC_OJS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NO_DOC_OJS()
        {

        }
        public NO_DOC_OJS(XElement root)
        {
            try
            {
                this._root= root;
                this._Text = root.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "URI_LIST")]
    public class URI_LIST
    {
        [XmlElement(ElementName = "URI_DOC")]
        public List<URI_DOC>? _URI_DOC { get; set; } = new List<URI_DOC>();
    }

    [XmlRoot(ElementName = "URI_DOC")]
    public class URI_DOC
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "LG")]
        public string? _LG { get; set; } = string.Empty;

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public URI_DOC()
        {

        }
        public URI_DOC(XElement root)
        {
            try
            {
                this._root  = root;
                this._Text= root.Value;
                this._LG = root.Attribute("LG")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
                this._LG = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "LG_ORIG")]
    public class LG_ORIG
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public LG_ORIG()
        {

        }
        public LG_ORIG(XElement root)
        {
            try
            {
                this._root= root;
                this._Text= root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "ISO_COUNTRY")]
    public class ISO_COUNTRY
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "VALUE")]
        public string? _VALUE { get; set; } = string.Empty;

        //ctor
        public ISO_COUNTRY()
        {

        }
        public ISO_COUNTRY(XElement root)
        {
            try
            {
                this._root = root;
                this._VALUE = root.Attribute("VALUE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._VALUE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "IA_URL_GENERAL")]
    public class IA_URL_GENERAL
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public IA_URL_GENERAL()
        {

        }
        public IA_URL_GENERAL(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text= String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "IA_URL_ETENDERING")]
    public class IA_URL_ETENDERING
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public IA_URL_ETENDERING()
        {

        }
        public IA_URL_ETENDERING(XElement root)
        {
            try
            {
                this._root= root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "ORIGINAL_CPV")]
    public class ORIGINAL_CPV
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public ORIGINAL_CPV()
        {

        }
        public ORIGINAL_CPV(XElement root)
        {
            try
            {
                this._root = root;
                this._CODE = root.Attribute("CODE")!.Value;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
                this._CODE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "ORIGINAL_NUTS")]
    public class ORIGINAL_NUTS
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public ORIGINAL_NUTS()
        {

        }
        public ORIGINAL_NUTS(XElement root)
        {
            try
            {
                this._root = root;
                this._CODE = root.Attribute("CODE")!.Value;
                this._Text= root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._CODE= String.Empty;
                this._Text = String.Empty;
            }
        }

    }

    [XmlRoot(ElementName = "CA_CE_NUTS")]
    public class CA_CE_NUTS
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public CA_CE_NUTS()
        {

        }
        public CA_CE_NUTS(XElement root)
        {
            try
            {
                this._root= root;
                this._CODE = root.Attribute("CODE")!.Value;
                this._Text = root.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._CODE= String.Empty;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "PERFORMANCE_NUTS")]
    public class PERFORMANCE_NUTS
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public PERFORMANCE_NUTS()
        {

        }
        public PERFORMANCE_NUTS(XElement root)
        {
            try
            {
                this._root = root;
                this._CODE = root.Attribute("CODE")!.Value;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._CODE= String.Empty;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "VALUES")]
    public class VALUES
    {
        [XmlElement(ElementName = "VALUE")]
        public List<VALUE>? _VALUE { get; set; } = new List<VALUE>();
    }
    #region VALUES
    [XmlRoot(ElementName = "VALUE")]
    public class VALUE
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "TYPE")]
        public string? _TYPE { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "CURRENCY")]
        public string? _CURRENCY { get; set; } = string.Empty;

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public VALUE()
        {

        }
        public VALUE(XElement root)
        {
            try
            {
                this._root= root;
                this._TYPE = root.Attribute("TYPE")!.Value;
                this._CURRENCY = root.Attribute("CURRENCY")!.Value;
                this._Text= root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._TYPE= String.Empty;
                this._CURRENCY= String.Empty;
                this._Text = String.Empty;
            }
        }
    }
    #endregion VALUES

    #endregion [1] CODED_DATA_SECTION [2] NOTICE_DATA


    [XmlRoot(ElementName = "CODIF_DATA")]
    public class CODIF_DATA
    {
        //ELEMENTS
        [XmlElement(ElementName = "DS_DATE_DISPATCH")]
        public DS_DATE_DISPATCH? _DS_DATE_DISPATCH { get; set; } = new DS_DATE_DISPATCH();

        [XmlElement(ElementName = "DT_DATE_FOR_SUBMISSION")]
        public DT_DATE_FOR_SUBMISSION? _DT_DATE_FOR_SUBMISSION { get; set; } = new DT_DATE_FOR_SUBMISSION();

        [XmlElement(ElementName = "AA_AUTHORITY_TYPE")]
        public AA_AUTHORITY_TYPE? _AA_AUTHORITY_TYPE { get; set; } = new AA_AUTHORITY_TYPE();

        [XmlElement(ElementName = "TD_DOCUMENT_TYPE")]
        public TD_DOCUMENT_TYPE? _TD_DOCUMENT_TYPE { get; set; } = new TD_DOCUMENT_TYPE();

        [XmlElement(ElementName = "NC_CONTRACT_NATURE")]
        public NC_CONTRACT_NATURE? _NC_CONTRACT_NATURE { get; set; } = new NC_CONTRACT_NATURE();

        [XmlElement(ElementName = "PR_PROC")]
        public PR_PROC? _PR_PROC { get; set; } = new PR_PROC();

        [XmlElement(ElementName = "RP_REGULATION")]
        public RP_REGULATION? _RP_REGULATION { get; set; } = new RP_REGULATION();

        [XmlElement(ElementName = "TY_TYPE_BID")]
        public TY_TYPE_BID? _TY_TYPE_BID { get; set; } = new TY_TYPE_BID();

        [XmlElement(ElementName = "AC_AWARD_CRIT")]
        public AC_AWARD_CRIT? _AC_AWARD_CRIT { get; set; } = new AC_AWARD_CRIT();

        [XmlElement(ElementName = "MA_MAIN_ACTIVITIES")]
        public MA_MAIN_ACTIVITIES? _MA_MAIN_ACTIVITIES { get; set; } = new MA_MAIN_ACTIVITIES();

        [XmlElement(ElementName = "HEADING")]
        public HEADING? _HEADING { get; set; } = new HEADING();

        [XmlElement(ElementName = "INITIATOR")]
        public INITIATOR? _INITIATOR { get; set; } = new INITIATOR();

        [XmlElement(ElementName = "DIRECTIVE")]
        public DIRECTIVE? _DIRECTIVE { get; set; } = new DIRECTIVE();
    }

    #region [1] CODED_DATA_SECTION [2] CODIF_DATA
    [XmlRoot(ElementName = "DS_DATE_DISPATCH")]
    public class DS_DATE_DISPATCH
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public DS_DATE_DISPATCH()
        {

        }
        public DS_DATE_DISPATCH(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "DT_DATE_FOR_SUBMISSION")]
    public class DT_DATE_FOR_SUBMISSION
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public DT_DATE_FOR_SUBMISSION()
        {

        }
        public DT_DATE_FOR_SUBMISSION(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "AA_AUTHORITY_TYPE")]
    public class AA_AUTHORITY_TYPE
    {
        public XElement? _root { get; set;}

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public AA_AUTHORITY_TYPE()
        {

        }
        public AA_AUTHORITY_TYPE(XElement root)
        {
            try
            {
                this._root=root;
                this._CODE = root.Attribute("CODE")!.Value;
                this._Text = root.Value.ToString();
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
                this._CODE=String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "TD_DOCUMENT_TYPE")]
    public class TD_DOCUMENT_TYPE
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public TD_DOCUMENT_TYPE()
        {

        }
        public TD_DOCUMENT_TYPE(XElement root)
        {
            try
            {
                this._root = root;
                this._Text=root.Value.ToString();
                this._CODE = root.Attribute("CODE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
                this._CODE=String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NC_CONTRACT_NATURE")]
    public class NC_CONTRACT_NATURE
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NC_CONTRACT_NATURE()
        {

        }
        public NC_CONTRACT_NATURE(XElement root)
        {
            try
            {
                this._root=root;
                this._Text=root.Value.ToString();
                this._CODE = root.Attribute("CODE")!.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
                this._CODE=String.Empty;
            }
        }

    }

    [XmlRoot(ElementName = "PR_PROC")]
    public class PR_PROC
    {
        public XElement? _root { get; set;}

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public PR_PROC()
        {

        }
        public PR_PROC(XElement root)
        {
            try
            {
                this._root=root;
                this._Text=root.Value.ToString();
                this._CODE = root.Attribute("CODE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text= String.Empty;
                this._CODE=String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "RP_REGULATION")]
    public class RP_REGULATION
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public RP_REGULATION()
        {

        }
        public RP_REGULATION(XElement root)
        {
            try
            {
                this._root= root;
                this._Text=root.Value.ToString();
                this._CODE = root.Attribute("CODE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text= String.Empty;
                this._CODE=String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "TY_TYPE_BID")]
    public class TY_TYPE_BID
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public TY_TYPE_BID()
        {

        }
        public TY_TYPE_BID(XElement root)
        {
            try
            {
                this._root = root;
                this._Text= root.Value.ToString();
                this._CODE = root.Attribute("CODE")!.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text= String.Empty;
                this._CODE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "AC_AWARD_CRIT")]
    public class AC_AWARD_CRIT
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public AC_AWARD_CRIT()
        {

        }
        public AC_AWARD_CRIT(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
                this._CODE = root.Attribute("CODE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
                this._CODE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "MA_MAIN_ACTIVITIES")]
    public class MA_MAIN_ACTIVITIES
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;
        
        //ctor
        public MA_MAIN_ACTIVITIES()
        {

        }
        public MA_MAIN_ACTIVITIES(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
                this._CODE = root.Attribute("CODE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
                this._CODE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "HEADING")]
    public class HEADING
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public HEADING()
        {

        }
        public HEADING(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "INITIATOR")]
    public class INITIATOR
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public INITIATOR()
        {

        }
        public INITIATOR(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    //En TRANSLATION_SECTION y FORM_SECTION/F12_2014
    [XmlRoot(ElementName = "DIRECTIVE")]
    public class DIRECTIVE
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "VALUE")]
        public string? _VALUE { get; set; } = string.Empty;

        //ctor
        public DIRECTIVE()
        {

        }
        public DIRECTIVE(XElement root)
        {
            try
            {
                this._root = root;
                this._VALUE = root.Attribute("VALUE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._VALUE = String.Empty;
            }
        }
    }
    #endregion [1] CODED_DATA_SECTION [2] CODIF_DATA
    #endregion [1] CODED_DATA_SECTION



    /// <summary>
    ///*[1] TRANSLATION_SECTION
    ///     [2] ML_TITLES
    ///     [2] ML_AA_NAMES
    /// </summary>
    #region [1] TRANSLATION_SECTION
    [XmlRoot(ElementName = "ML_TITLES")]
    public class ML_TITLES
    {
        [XmlElement(ElementName = "ML_TI_DOC")]
        public List<ML_TI_DOC>? _ML_TI_DOC { get; set; } = new List<ML_TI_DOC>();
    }

    #region [1] TRANSLATION_SECTION [2] ML_TITLES
    [XmlRoot(ElementName = "ML_TI_DOC")]
    public class ML_TI_DOC
    {
        public XElement? _root { get; set; }

        //ELEMENTS
        [XmlElement(ElementName = "TI_CY")]
        public TI_CY? _TI_CY { get; set; } = new TI_CY();

        [XmlElement(ElementName = "TI_TOWN")]
        public TI_TOWN? _TI_TOWN { get; set; } = new TI_TOWN();

        [XmlElement(ElementName = "TI_TEXT")]
        public TI_TEXT? _TI_TEXT { get; set; } = new TI_TEXT();

        //ATTRIBUTES       
        [XmlAttribute(AttributeName = "LG")]
        public string? _LG { get; set; } = string.Empty;

        //ctor
        public ML_TI_DOC()
        {

        }
        public ML_TI_DOC(XElement root)
        {
            try
            {
                this._root = root;
                this._LG = root.Attribute("LG")!.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._LG = string.Empty;
            }
        }
    }

    #region [1] TRANSLATION_SECTION [2] ML_TITLES [3] ML_TI_DOC   
    [XmlRoot(ElementName = "TI_CY")]
    public class TI_CY
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public TI_CY()
        {

        }
        public TI_CY(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "TI_TOWN")]
    public class TI_TOWN
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public TI_TOWN()
        {

        }
        public TI_TOWN(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }

        }
    }

    [XmlRoot(ElementName = "TI_TEXT")]
    public class TI_TEXT
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "P")]
    public class P
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public P()
        {

        }
        public P(XElement root)
        {
            try
            {
                this._root=root;
                this._Text=root.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text=string.Empty;
            }
        }
    }

    #endregion [1] TRANSLATION_SECTION [2] ML_TITLE S[3] ML_TI_DOC

    #endregion [1] TRANSLATION_SECTION [2] ML_TITLES

    [XmlRoot(ElementName = "ML_AA_NAMES")]
    public class ML_AA_NAMES
    {
        [XmlElement(ElementName = "AA_NAME")]
        public List<AA_NAME>? _AA_NAME { get; set; } = new List<AA_NAME>();
    }

    // [1] TRANSLATION_SECTION [2] ML_AA_NAMES
    [XmlRoot(ElementName = "AA_NAME")]
    public class AA_NAME
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "LG")]
        public string? _LG { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public AA_NAME()
        {

        }
        public AA_NAME(XElement root)
        {
            try
            {
                this._root  = root;
                this._Text=root.Value;
                this._LG = root.Attribute("LG")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text=string.Empty;
                this._LG=string.Empty;
            }
        }
    }

    #endregion [1] TRANSLATION_SECTION



    /// <summary>
    ///*[1] FORM_SECTION
    ///     [2] F12_2014
    /// </summary>
    #region [1] FORM_SECTION
    [XmlRoot(ElementName = "F12_2014")]
    public class FORM
    {
        public XElement? _root { get; set; }

        //ELEMENTS
        [XmlElement(ElementName = "LEGAL_BASIS")]
        public LEGAL_BASIS? _LEGAL_BASIS { get; set; } = new LEGAL_BASIS();

        [XmlElement(ElementName = "DIRECTIVE")]
        public DIRECTIVE? _DIRECTIVE { get; set; } = new DIRECTIVE();

        [XmlElement(ElementName = "CONTRACTING_BODY")]
        public CONTRACTING_BODY? _CONTRACTING_BODY { get; set; } = new CONTRACTING_BODY();

        [XmlElement(ElementName = "OBJECT_CONTRACT")]
        public OBJECT_CONTRACT? _OBJECT_CONTRACT { get; set; } = new OBJECT_CONTRACT();

        [XmlElement(ElementName = "LEFTI")]
        public LEFTI? _LEFTI { get; set; } = new LEFTI();

        [XmlElement(ElementName = "PROCEDURE")]
        public PROCEDURE? _PROCEDURE { get; set; } = new PROCEDURE();

        [XmlElement(ElementName = "COMPLEMENTARY_INFO")]
        public COMPLEMENTARY_INFO? _COMPLEMENTARY_INFO { get; set; } = new COMPLEMENTARY_INFO();
        
        //ATTRIBUTES       
        [XmlAttribute(AttributeName = "CATEGORY")]
        public string? _CATEGORY { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "FORM")]
        public string? _FORM { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "LG")]
        public string? _LG { get; set; } = string.Empty;

        //ctor
        public FORM()
        {

        }
        public FORM(XElement root)
        {
            try
            {
                this._root = root;
                this._CATEGORY = root.Attribute("CATEGORY")!.Value;
                this._FORM = root.Attribute("FORM")!.Value;
                this._LG = root.Attribute("LG")!.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._CATEGORY = String.Empty;
                this._FORM = String.Empty;
                this._LG = String.Empty;
            }
        }
    }
    
    //NOTA: no debería de borrar esto ya que tienen rutas XML diferentes
    [XmlRoot(ElementName = "F02_2014")]
    public class F02_2014
    {
        public XElement? _root { get; set; }

        //ATTRIBUTES  
        [XmlAttribute(AttributeName = "CATEGORY")]
        public string? _CATEGORY { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "FORM")]
        public string? _FORM { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "LG")]
        public string? _LG { get; set; } = string.Empty;

        //ELEMENTS
        [XmlElement(ElementName = "LEGAL_BASIS")]
        public LEGAL_BASIS? _LEGAL_BASIS { get; set; } = new LEGAL_BASIS();

        [XmlElement(ElementName = "DIRECTIVE")]
        public DIRECTIVE? _DIRECTIVE { get; set; } = new DIRECTIVE();

        [XmlElement(ElementName = "CONTRACTING_BODY")]
        public CONTRACTING_BODY? _CONTRACTING_BODY { get; set; } = new CONTRACTING_BODY();

        [XmlElement(ElementName = "OBJECT_CONTRACT")]
        public OBJECT_CONTRACT? _OBJECT_CONTRACT { get; set; } = new OBJECT_CONTRACT();

        [XmlElement(ElementName = "LEFTI")]
        public LEFTI? _LEFTI { get; set; } = new LEFTI();

        [XmlElement(ElementName = "PROCEDURE")]
        public PROCEDURE? _PROCEDURE { get; set; } = new PROCEDURE();

        [XmlElement(ElementName = "COMPLEMENTARY_INFO")]
        public COMPLEMENTARY_INFO? _COMPLEMENTARY_INFO { get; set; } = new COMPLEMENTARY_INFO();

        //ctor
        public F02_2014()
        {

        }
        public F02_2014(XElement root)
        {
            try
            {
                this._root= root;
                this._CATEGORY = root.Attribute("CATEGORY")!.Value;
                this._FORM = root.Attribute("FORM")!.Value;
                this._LG = root.Attribute("LG")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._CATEGORY = String.Empty;
                this._FORM = String.Empty;
                this._LG = String.Empty;
            }
        }
    }

    /// <summary>
    /// [1] FORM_SECTION
    ///  *  [2] F12_2014
    ///         [3] LEGAL_BASIS
    ///         [3] CONTRACTING_BODY
    ///         [3] OBJECT_CONTRACT
    ///         [3] LEFTI
    ///         [3] PROCEDURE
    ///         [3] COMPLEMENTARY_INFO
    /// </summary>
    #region [1] FORM_SECTION [2] F
    [XmlRoot(ElementName = "LEGAL_BASIS")]
    public class LEGAL_BASIS
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "VALUE")]
        public string? _VALUE { get; set; } = string.Empty;

        //ctor
        public LEGAL_BASIS()
        {

        }
        public LEGAL_BASIS(XElement root)
        {
            try
            {
                this._root = root!;
                this._VALUE = root.Attribute("VALUE")!.Value;   
            }catch(NullReferenceException)
            {
                this._root = null;
                this._VALUE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "CONTRACTING_BODY")]
    public class CONTRACTING_BODY
    {
        //ELEMENTS
        [XmlElement(ElementName = "DOCUMENT_FULL")]
        public DOCUMENT_FULL? _DOCUMENT_FULL { get; set; } = new DOCUMENT_FULL();

        [XmlElement(ElementName = "URL_DOCUMENT")]
        public URL_DOCUMENT? _URL_DOCUMENT { get; set; } = new URL_DOCUMENT();

        [XmlElement(ElementName = "ADDRESS_CONTRACTING_BODY")]
        public ADDRESS_CONTRACTING_BODY? _ADDRESS_CONTRACTING_BODY { get; set; } = new ADDRESS_CONTRACTING_BODY();

        [XmlElement(ElementName = "ADDRESS_FURTHER_INFO")]
        public ADDRESS_FURTHER_INFO? _ADDRESS_FURTHER_INFO { get; set; } = new ADDRESS_FURTHER_INFO();

        [XmlElement(ElementName = "ADDRESS_PARTICIPATION")]
        public ADDRESS_PARTICIPATION? _ADDRESS_PARTICIPATION { get; set; } = new ADDRESS_PARTICIPATION();

        [XmlElement(ElementName = "CA_TYPE")]
        public CA_TYPE? _CA_TYPE{ get; set; } = new CA_TYPE();

        [XmlElement(ElementName = "CA_TYPE_OTHER")]
        public CA_TYPE_OTHER? _CA_TYPE_OTHER { get; set; } = new CA_TYPE_OTHER();

        [XmlElement(ElementName = "CA_ACTIVITY")]
        public CA_ACTIVITY? _CA_ACTIVITY { get; set; } = new CA_ACTIVITY();

        [XmlElement(ElementName = "URL_PARTICIPATION")]
        public URL_PARTICIPATION? _URL_PARTICIPATION { get; set; } = new URL_PARTICIPATION();

    }

    #region [1] FORM_SECTION [2] F12_2014 [3] CONTRACTING_BODY
    [XmlRoot(ElementName = "DOCUMENT_FULL")]
    public class DOCUMENT_FULL
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public DOCUMENT_FULL()
        {

        }
        public DOCUMENT_FULL(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "URL_DOCUMENT")]
    public class URL_DOCUMENT
    {
        public XElement? _root { get; set; }
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public URL_DOCUMENT()
        {

        }
        public URL_DOCUMENT(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text= string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "URL_PARTICIPATION")]
    public class URL_PARTICIPATION
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public URL_PARTICIPATION()
        {

        }
        public URL_PARTICIPATION(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "ADDRESS_CONTRACTING_BODY")]
    public class ADDRESS_CONTRACTING_BODY
    {
        [XmlElement(ElementName = "OFFICIALNAME")]
        public OFFICIALNAME? _OFFICIALNAME { get; set; } = new OFFICIALNAME();

        [XmlElement(ElementName = "NATIONALID")]
        public NATIONALID? _NATIONALID { get; set; } = new NATIONALID();

        [XmlElement(ElementName = "ADDRESS")]
        public ADDRESS? _ADDRESS { get; set; } = new ADDRESS();

        [XmlElement(ElementName = "TOWN")]
        public TOWN? _TOWN { get; set; } = new TOWN();

        [XmlElement(ElementName = "POSTAL_CODE")]
        public POSTAL_CODE? _POSTAL_CODE { get; set; } = new POSTAL_CODE();

        [XmlElement(ElementName = "COUNTRY")]
        public COUNTRY? _COUNTRY { get; set; } = new COUNTRY();

        [XmlElement(ElementName = "CONTACT_POINT")]
        public CONTACT_POINT? _CONTACT_POINT { get; set; } = new CONTACT_POINT();

        [XmlElement(ElementName = "PHONE")]
        public PHONE? _PHONE { get; set; } = new PHONE();

        [XmlElement(ElementName = "E_MAIL")]
        public E_MAIL? _E_MAIL { get; set; } = new E_MAIL();

        [XmlElement(ElementName = "FAX")]
        public FAX? _FAX { get; set; } = new FAX();

        [XmlElement(ElementName = "NUTS")]
        public NUTS? _NUTS { get; set; } = new NUTS();

        [XmlElement(ElementName = "URL_GENERAL")]
        public URL_GENERAL? _URL_GENERAL { get; set; } = new URL_GENERAL();

        [XmlElement(ElementName = "URL_BUYER")]
        public URL_BUYER? _URL_BUYER { get; set; } = new URL_BUYER();
    }

    [XmlRoot(ElementName = "ADDRESS_FURTHER_INFO")]
    public class ADDRESS_FURTHER_INFO
    {
        [XmlElement(ElementName = "OFFICIALNAME")]
        public OFFICIALNAME? _OFFICIALNAME { get; set; } = new OFFICIALNAME();

        [XmlElement(ElementName = "NATIONALID")]
        public NATIONALID? _NATIONALID { get; set; } = new NATIONALID();

        [XmlElement(ElementName = "ADDRESS")]
        public ADDRESS? _ADDRESS { get; set; } = new ADDRESS();

        [XmlElement(ElementName = "TOWN")]
        public TOWN? _TOWN { get; set; } = new TOWN();

        [XmlElement(ElementName = "POSTAL_CODE")]
        public POSTAL_CODE? _POSTAL_CODE { get; set; } = new POSTAL_CODE();

        [XmlElement(ElementName = "COUNTRY")]
        public COUNTRY? _COUNTRY { get; set; } = new COUNTRY();

        [XmlElement(ElementName = "CONTACT_POINT")]
        public CONTACT_POINT? _CONTACT_POINT { get; set; } = new CONTACT_POINT();

        [XmlElement(ElementName = "PHONE")]
        public PHONE? _PHONE { get; set; } = new PHONE();

        [XmlElement(ElementName = "E_MAIL")]
        public E_MAIL? _E_MAIL { get; set; } = new E_MAIL();

        [XmlElement(ElementName = "FAX")]
        public FAX? _FAX { get; set; } = new FAX();

        [XmlElement(ElementName = "NUTS")]
        public NUTS? _NUTS { get; set; } = new NUTS();

        [XmlElement(ElementName = "URL_GENERAL")]
        public URL_GENERAL? _URL_GENERAL { get; set; } = new URL_GENERAL();

        [XmlElement(ElementName = "URL_BUYER")]
        public URL_BUYER? _URL_BUYER { get; set; } = new URL_BUYER();

    }

    [XmlRoot(ElementName = "ADDRESS_PARTICIPATION")]
    public class ADDRESS_PARTICIPATION
    {
        [XmlElement(ElementName = "OFFICIALNAME")]
        public OFFICIALNAME? _OFFICIALNAME { get; set; } = new OFFICIALNAME();

        [XmlElement(ElementName = "NATIONALID")]
        public NATIONALID? _NATIONALID { get; set; } = new NATIONALID();

        [XmlElement(ElementName = "ADDRESS")]
        public ADDRESS? _ADDRESS { get; set; } = new ADDRESS();

        [XmlElement(ElementName = "TOWN")]
        public TOWN? _TOWN { get; set; } = new TOWN();

        [XmlElement(ElementName = "POSTAL_CODE")]
        public POSTAL_CODE? _POSTAL_CODE { get; set; } = new POSTAL_CODE();

        [XmlElement(ElementName = "COUNTRY")]
        public COUNTRY? _COUNTRY { get; set; } = new COUNTRY();

        [XmlElement(ElementName = "CONTACT_POINT")]
        public CONTACT_POINT? _CONTACT_POINT { get; set; } = new CONTACT_POINT();

        [XmlElement(ElementName = "PHONE")]
        public PHONE? _PHONE { get; set; } = new PHONE();

        [XmlElement(ElementName = "E_MAIL")]
        public E_MAIL? _E_MAIL { get; set; } = new E_MAIL();

        [XmlElement(ElementName = "FAX")]
        public FAX? _FAX { get; set; } = new FAX();

        [XmlElement(ElementName = "NUTS")]
        public NUTS? _NUTS { get; set; } = new NUTS();

        [XmlElement(ElementName = "URL_GENERAL")]
        public URL_GENERAL? _URL_GENERAL { get; set; } = new URL_GENERAL();

        [XmlElement(ElementName = "URL_BUYER")]
        public URL_BUYER? _URL_BUYER { get; set; } = new URL_BUYER();
    }

    [XmlRoot(ElementName = "CA_TYPE")]
    public class CA_TYPE
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "VALUE")]
        public string? _VALUE { get; set; } = string.Empty;

        //ctor
        public CA_TYPE()
        {

        }
        public CA_TYPE(XElement root)
        {
            try
            {
                this._root= root;
                this._VALUE = root.Attribute("VALUE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._VALUE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "CA_TYPE_OTHER")]
    public class CA_TYPE_OTHER
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public CA_TYPE_OTHER()
        {

        }
        public CA_TYPE_OTHER(XElement root)
        {
            try
            {
                this._root= root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "CA_ACTIVITY")]
    public class CA_ACTIVITY
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "VALUE")]
        public string? _VALUE { get; set; } = string.Empty;

        //ctor
        public CA_ACTIVITY()
        {

        }
        public CA_ACTIVITY(XElement root)
        {
            try
            {
                this._root= root;
                this._VALUE = root.Attribute("VALUE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._VALUE = String.Empty;
            }
        }
    }

    #region [1] FORM_SECTION [2] F12_2014 [3] CONTRACTING_BODY [4] ADDRESS_
    [XmlRoot(ElementName = "OFFICIALNAME")]
    public class OFFICIALNAME
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public OFFICIALNAME()
        {
        }
        public OFFICIALNAME(XElement root)
        {
            try
            {
                this._root= root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text= String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NATIONALID")]
    public class NATIONALID
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NATIONALID()
        {

        }
        public NATIONALID(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "ADDRESS")]
    public class ADDRESS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public ADDRESS()
        {

        }
        public ADDRESS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "TOWN")]
    public class TOWN
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public TOWN()
        {

        }
        public TOWN(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "POSTAL_CODE")]
    public class POSTAL_CODE
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public POSTAL_CODE()
        {

        }
        public POSTAL_CODE(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "COUNTRY")]
    public class COUNTRY
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "VALUE")]
        public string? _VALUE { get; set; } = string.Empty;

        //ctor
        public COUNTRY()
        {

        }
        public COUNTRY(XElement root)
        {
            try
            {
                this._root = root;
                this._VALUE = root.Attribute("VALUE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._VALUE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "CONTACT_POINT")]
    public class CONTACT_POINT
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public CONTACT_POINT()
        {

        }
        public CONTACT_POINT(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "PHONE")]
    public class PHONE
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public PHONE()
        {

        }
        public PHONE(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "E_MAIL")]
    public class E_MAIL
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public E_MAIL()
        {

        }
        public E_MAIL(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "FAX")]
    public class FAX
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public FAX()
        {

        }
        public FAX(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NUTS")]
    public class NUTS
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;

        //ctor
        public NUTS()
        {

        }
        public NUTS(XElement root)
        {
            try
            {
                this._root = root;
                this._CODE = root.Attribute("CODE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._CODE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "URL_GENERAL")]
    public class URL_GENERAL
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public URL_GENERAL()
        {

        }
        public URL_GENERAL(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "URL_BUYER")]
    public class URL_BUYER
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public URL_BUYER()
        {

        }
        public URL_BUYER(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }
    #endregion [1] FORM_SECTION [2] F12_2014 [3] CONTRACTING_BODY [4] ADDRESS_

    #endregion [1] FORM_SECTION [2] F12_2014 [3] CONTRACTING_BODY

    [XmlRoot(ElementName = "OBJECT_CONTRACT")]
    public class OBJECT_CONTRACT
    {
        //ELEMENTS
        [XmlElement(ElementName = "TITLE")]
        public TITLE? _TITLE { get; set; } = new TITLE();

        [XmlElement(ElementName = "REFERENCE_NUMBER")]
        public REFERENCE_NUMBER? _REFERENCE_NUMBER { get; set; } = new REFERENCE_NUMBER();

        [XmlElement(ElementName = "CPV_MAIN")]
        public CPV_MAIN? _CPV_MAIN { get; set; } = new CPV_MAIN();

        [XmlElement(ElementName = "OBJECT_DESCR")]
        public OBJECT_DESCR? _OBJECT_DESCR { get; set; } = new OBJECT_DESCR();

        [XmlElement(ElementName = "TYPE_CONTRACT")]
        public TYPE_CONTRACT? _TYPE_CONTRACT { get; set; } = new TYPE_CONTRACT();

        [XmlElement(ElementName = "SHORT_DESCR")]
        public SHORT_DESCR? _SHORT_DESCR { get; set; } = new SHORT_DESCR();

        [XmlElement(ElementName = "NO_LOT_DIVISION")]
        public NO_LOT_DIVISION? _NO_LOT_DIVISION { get; set; } = new NO_LOT_DIVISION();

        [XmlElement(ElementName = "VAL_ESTIMATED_TOTAL")]
        public VAL_ESTIMATED_TOTAL? _VAL_ESTIMATED_TOTAL { get; set; } = new VAL_ESTIMATED_TOTAL();

        [XmlElement(ElementName = "LOT_DIVISION")]
        public LOT_DIVISION? _LOT_DIVISION { get; set; } = new LOT_DIVISION();

    }

    #region [1] FORM_SECTION [2] F12_2014 [3] OBJECT_CONTRACT
    [XmlRoot(ElementName = "TITLE")]
    public class TITLE
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "REFERENCE_NUMBER")]
    public class REFERENCE_NUMBER
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public REFERENCE_NUMBER()
        {

        }
        public REFERENCE_NUMBER(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "CPV_MAIN")]
    public class CPV_MAIN
    {
        [XmlElement(ElementName = "CPV_CODE")]
        public CPV_CODE? _CPV_CODE { get; set; } = new CPV_CODE();
    }

    #region [1] FORM_SECTION [2] F12_2014 [3] OBJECT_CONTRACT [4] CPV_MAIN
    [XmlRoot(ElementName = "CPV_CODE")]
    public class CPV_CODE
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CODE")]
        public string? _CODE { get; set; } = string.Empty;

        //ctor
        public CPV_CODE()
        {

        }
        public CPV_CODE(XElement root)
        {
            try
            {
                this._root = root;
                this._CODE = root.Attribute("CODE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._CODE= String.Empty;
            }
        }

    }
    #endregion [1] FORM_SECTION [2] F12_2014 [3] OBJECT_CONTRACT [4] CPV_MAIN

    [XmlRoot(ElementName = "OBJECT_DESCR")]
    public class OBJECT_DESCR
    {
        [XmlElement(ElementName = "CPV_ADDITIONAL")]
        public List<CPV_ADDITIONAL>? _CPV_ADDITIONAL { get; set; } = new List<CPV_ADDITIONAL>();

        [XmlElement(ElementName = "SHORT_DESCR")]
        public SHORT_DESCR? _SHORT_DESCR { get; set; } = new SHORT_DESCR();

        [XmlElement(ElementName = "NO_EU_PROGR_RELATED")]
        public NO_EU_PROGR_RELATED? _NO_EU_PROGR_RELATED { get; set; } = new NO_EU_PROGR_RELATED();

        [XmlElement(ElementName = "NUTS")]
        public NUTS? _NUTS { get; set; } = new NUTS();

        [XmlElement(ElementName = "MAIN_SITE")]
        public MAIN_SITE? _MAIN_SITE { get; set; } = new MAIN_SITE();

        [XmlElement(ElementName = "AC")]
        public AC? _AC { get; set; } = new AC();

        [XmlElement(ElementName = "DURATION")]
        public DURATION? _DURATION { get; set; } = new DURATION();

        [XmlElement(ElementName = "NO_RENEWAL")]
        public NO_RENEWAL? _NO_RENEWAL { get; set; } = new NO_RENEWAL();

        [XmlElement(ElementName = "NB_ENVISAGED_CANDIDATE")]
        public NB_ENVISAGED_CANDIDATE? _NB_ENVISAGED_CANDIDATE { get; set; } = new NB_ENVISAGED_CANDIDATE();

        [XmlElement(ElementName = "CRITERIA_CANDIDATE")]
        public CRITERIA_CANDIDATE? _CRITERIA_CANDIDATE { get; set; } = new CRITERIA_CANDIDATE();

        [XmlElement(ElementName = "NO_ACCEPTED_VARIANTS")]
        public NO_ACCEPTED_VARIANTS? _NO_ACCEPTED_VARIANTS { get; set; } = new NO_ACCEPTED_VARIANTS();

        [XmlElement(ElementName = "OPTIONS")]
        public OPTIONS? _OPTIONS { get; set; } = new OPTIONS();

        [XmlElement(ElementName = "OPTIONS_DESCR")]
        public OPTIONS_DESCR? _OPTIONS_DESCR { get; set; } = new OPTIONS_DESCR();

        [XmlAttribute(AttributeName = "ITEM")]
        public ITEM? _ITEM { get; set; } = new ITEM();

        [XmlElement(ElementName = "TITLE")]
        public TITLE? _TITLE { get; set; } = new TITLE();

        [XmlElement(ElementName = "LOT_NO")]
        public LOT_NO? _LOT_NO { get; set; } = new LOT_NO();

        [XmlElement(ElementName = "VAL_OBJECT")]
        public VAL_OBJECT? _VAL_OBJECT { get; set; } = new VAL_OBJECT();

        [XmlElement(ElementName = "RENEWAL")]
        public RENEWAL? _RENEWAL { get; set; } = new RENEWAL();

        [XmlElement(ElementName = "RENEWAL_DESCR")]
        public RENEWAL_DESCR? _RENEWAL_DESCR { get; set; } = new RENEWAL_DESCR();

        [XmlElement(ElementName = "INFO_ADD")]
        public INFO_ADD? _INFO_ADD { get; set; } = new INFO_ADD();
    }

    #region [1] FORM_SECTION [2] F12_2014 [3] OBJECT_CONTRACT [4] OBJECT_DESCR
    [XmlRoot(ElementName = "CPV_ADDITIONAL")]
    public class CPV_ADDITIONAL
    {
        [XmlElement(ElementName = "CPV_CODE")]
        public List<CPV_CODE>? _CPV_CODE_LIST { get; set; } = new List<CPV_CODE>();
    }

    [XmlRoot(ElementName = "SHORT_DESCR")]
    public class SHORT_DESCR
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "NO_EU_PROGR_RELATED")]
    public class NO_EU_PROGR_RELATED
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NO_EU_PROGR_RELATED()
        {

        }
        public NO_EU_PROGR_RELATED(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "MAIN_SITE")]
    public class MAIN_SITE
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "AC")]
    public class AC
    {
        [XmlElement(ElementName = "AC_QUALITY")]
        public AC_QUALITY? _AC_QUALITY { get; set; } = new AC_QUALITY();

        [XmlElement(ElementName = "AC_PRICE")]
        public AC_PRICE? _AC_PRICE { get; set; } = new AC_PRICE();
    }

    #region AC
    [XmlRoot(ElementName = "AC_QUALITY")]
    public class AC_QUALITY
    {
        [XmlElement(ElementName = "AC_CRITERION")]
        public AC_CRITERION? _AC_CRITERION { get; set; } = new AC_CRITERION();

        [XmlElement(ElementName = "AC_WEIGHTING")]
        public AC_WEIGHTING? _AC_WEIGHTING { get; set; } = new AC_WEIGHTING();
    }

    [XmlRoot(ElementName = "AC_PRICE")]
    public class AC_PRICE
    {
        [XmlElement(ElementName = "AC_WEIGHTING")]
        public AC_WEIGHTING? _AC_WEIGHTING { get; set; } = new AC_WEIGHTING();
    }

    [XmlRoot(ElementName = "AC_CRITERION")]
    public class AC_CRITERION
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public AC_CRITERION()
        {

        }
        public AC_CRITERION(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "AC_WEIGHTING")]
    public class AC_WEIGHTING
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public AC_WEIGHTING()
        {

        }
        public AC_WEIGHTING(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }
    #endregion

    [XmlRoot(ElementName = "DURATION")]
    public class DURATION
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "TYPE")]
        public string? _TYPE { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public DURATION()
        {

        }
        public DURATION(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
                this._TYPE = root.Attribute("TYPE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
                this._TYPE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NO_RENEWAL")]
    public class NO_RENEWAL
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NO_RENEWAL()
        {

        }
        public NO_RENEWAL(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NB_ENVISAGED_CANDIDATE")]
    public class NB_ENVISAGED_CANDIDATE
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NB_ENVISAGED_CANDIDATE()
        {

        }
        public NB_ENVISAGED_CANDIDATE(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "CRITERIA_CANDIDATE")]
    public class CRITERIA_CANDIDATE
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "NO_ACCEPTED_VARIANTS")]
    public class NO_ACCEPTED_VARIANTS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NO_ACCEPTED_VARIANTS()
        {

        }
        public NO_ACCEPTED_VARIANTS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "OPTIONS")]
    public class OPTIONS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public OPTIONS()
        {

        }
        public OPTIONS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "OPTIONS_DESCR")]
    public class OPTIONS_DESCR
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "ITEM")]
    public class ITEM
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public ITEM()
        {

        }
        public ITEM(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "LOT_NO")]
    public class LOT_NO
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public LOT_NO()
        {

        }
        public LOT_NO(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "VAL_OBJECT")]
    public class VAL_OBJECT
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CURRENCY")]
        public string? _CURRENCY { get; set; } = string.Empty;

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public VAL_OBJECT()
        {

        }
        public VAL_OBJECT(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
                this._CURRENCY = root.Attribute("CURRENCY")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
                this._CURRENCY = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "RENEWAL")]
    public class RENEWAL
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public RENEWAL()
        {

        }
        public RENEWAL(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "RENEWAL_DESCR")]
    public class RENEWAL_DESCR
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    #endregion [1] FORM_SECTION [2] F12_2014 [3] OBJECT_CONTRACT [4] OBJECT_DESCR

    [XmlRoot(ElementName = "TYPE_CONTRACT")]
    public class TYPE_CONTRACT
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CTYPE")]
        public string? _CTYPE { get; set; } = string.Empty;

        //ctor
        public TYPE_CONTRACT()
        {

        }
        public TYPE_CONTRACT(XElement root)
        {
            try
            {
                this._root = root;
                this._CTYPE = root.Attribute("CTYPE")!.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._CTYPE = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NO_LOT_DIVISION")]
    public class NO_LOT_DIVISION
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NO_LOT_DIVISION()
        {

        }
        public NO_LOT_DIVISION(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "VAL_ESTIMATED_TOTAL")]
    public class VAL_ESTIMATED_TOTAL
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CURRENCY")]
        public string? _CURRENCY { get; set; } = string.Empty;
        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public VAL_ESTIMATED_TOTAL()
        {

        }
        public VAL_ESTIMATED_TOTAL(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
                this._CURRENCY = root.Attribute("CURRENCY")!.Value;
            }catch(NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
                this._CURRENCY = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "LOT_DIVISION")]
    public class LOT_DIVISION
    {
        [XmlElement(ElementName = "LOT_ALL")]
        public LOT_ALL? _LOT_ALL { get; set; } = new LOT_ALL();
    }

    [XmlRoot(ElementName = "LOT_ALL")]
    public class LOT_ALL
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public LOT_ALL()
        {

        }
        public LOT_ALL(XElement root)
        {
            try
            {
                this._root = root;
                this._Text=root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }
    #endregion [1] FORM_SECTION [2] F12_2014 [3] OBJECT_CONTRACT

    [XmlRoot(ElementName = "LEFTI")]
    public class LEFTI
    {
        [XmlElement(ElementName = "CRITERIA_SELECTION")]
        public CRITERIA_SELECTION? _CRITERIA_SELECTION { get; set; } = new CRITERIA_SELECTION();

        [XmlElement(ElementName = "PARTICULAR_PROFESSION")]
        public PARTICULAR_PROFESSION? _PARTICULAR_PROFESSION { get; set; } = new PARTICULAR_PROFESSION();

        [XmlElement(ElementName = "SUITABILITY")]
        public SUITABILITY? _SUITABILITY { get; set; } = new SUITABILITY();

        [XmlElement(ElementName = "ECONOMIC_FINANCIAL_INFO")]
        public ECONOMIC_FINANCIAL_INFO? _ECONOMIC_FINANCIAL_INFO { get; set; } = new ECONOMIC_FINANCIAL_INFO();

        [XmlElement(ElementName = "TECHNICAL_PROFESSIONAL_INFO")]
        public TECHNICAL_PROFESSIONAL_INFO? _TECHNICAL_PROFESSIONAL_INFO { get; set; } = new TECHNICAL_PROFESSIONAL_INFO();

        [XmlElement(ElementName = "REFERENCE_TO_LAW")]
        public REFERENCE_TO_LAW? _REFERENCE_TO_LAW { get; set; } = new REFERENCE_TO_LAW();

        [XmlElement(ElementName = "PERFORMANCE_CONDITIONS")]
        public PERFORMANCE_CONDITIONS? _PERFORMANCE_CONDITIONS { get; set; } = new PERFORMANCE_CONDITIONS();

        //TODO
        [XmlElement(ElementName = "PERFORMANCE_STAFF_QUALIFICATION")]
        public PERFORMANCE_STAFF_QUALIFICATION? _PERFORMANCE_STAFF_QUALIFICATION { get; set; } = new PERFORMANCE_STAFF_QUALIFICATION();
    }

    #region [1] FORM_SECTION [2] F12_2014 [3] LEFTI
    [XmlRoot(ElementName = "CRITERIA_SELECTION")]
    public class CRITERIA_SELECTION
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "PARTICULAR_PROFESSION")]
    public class PARTICULAR_PROFESSION
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "CTYPE")]
        public string? _CTYPE { get; set; } = string.Empty;

        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();

        //ctor
        public PARTICULAR_PROFESSION()
        {

        }
        public PARTICULAR_PROFESSION(XElement root)
        {
            try
            {
                this._root = root;
                this._CTYPE = root.Attribute("CTYPE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._CTYPE = string.Empty;
            }
        }
    }

    //f02
    [XmlRoot(ElementName = "SUITABILITY")]
    public class SUITABILITY
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "ECONOMIC_FINANCIAL_INFO")]
    public class ECONOMIC_FINANCIAL_INFO
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();

    }

    [XmlRoot(ElementName = "TECHNICAL_PROFESSIONAL_INFO")]
    public class TECHNICAL_PROFESSIONAL_INFO
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "REFERENCE_TO_LAW")]
    public class REFERENCE_TO_LAW
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "PERFORMANCE_CONDITIONS")]
    public class PERFORMANCE_CONDITIONS
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "PERFORMANCE_STAFF_QUALIFICATION")]
    public class PERFORMANCE_STAFF_QUALIFICATION
    {
        //TODO ¿que ha pasado aqui?
    }
    #endregion [1] FORM_SECTION [2] F12_2014 [3] LEFTI

    [XmlRoot(ElementName = "PROCEDURE")]
    public class PROCEDURE
    {
        //ELEMENTS
        //TODO REVISAR CONTRATOS
        [XmlElement(ElementName = "PT_RESTRICTED")]
        public PT_RESTRICTED? _PT_RESTRICTED { get; set; }
        /*
         * [XmlElement(ElementName = "PT_OPEN")]
         * public string PT_OPEN { get; set; }
         * 
         * 
         * PT_OPEN
         * PT_RESTRICTED
         * PT_COMPETITIVE_NEGOTIATION
         * PT_COMPETITIVE_DIALOGUE
         */
        public String? _first_element { get; set; } = string.Empty;

        [XmlElement(ElementName = "NB_PARTICIPANTS")]
        public NB_PARTICIPANTS? _NB_PARTICIPANTS { get; set; } = new NB_PARTICIPANTS();

        [XmlElement(ElementName = "NB_MIN_PARTICIPANTS")]
        public NB_MIN_PARTICIPANTS? _NB_MIN_PARTICIPANTS { get; set; } = new NB_MIN_PARTICIPANTS();

        [XmlElement(ElementName = "NB_MAX_PARTICIPANTS")]
        public NB_MAX_PARTICIPANTS? _NB_MAX_PARTICIPANTS { get; set; } = new NB_MAX_PARTICIPANTS();

        [XmlElement(ElementName = "CRITERIA_EVALUATION")]
        public CRITERIA_EVALUATION? _CRITERIA_EVALUATION { get; set; } = new CRITERIA_EVALUATION();

        [XmlElement(ElementName = "DATE_RECEIPT_TENDERS")]
        public DATE_RECEIPT_TENDERS? _DATE_RECEIPT_TENDERS { get; set; } = new DATE_RECEIPT_TENDERS();

        [XmlElement(ElementName = "TIME_RECEIPT_TENDERS")]
        public TIME_RECEIPT_TENDERS? _TIME_RECEIPT_TENDERS { get; set; } = new TIME_RECEIPT_TENDERS();

        [XmlElement(ElementName = "LANGUAGES")]
        public LANGUAGES? _LANGUAGES { get; set; } = new LANGUAGES();

        [XmlElement(ElementName = "PRIZE_AWARDED")]
        public PRIZE_AWARDED? _PRIZE_AWARDED { get; set; } = new PRIZE_AWARDED();

        [XmlElement(ElementName = "NUMBER_VALUE_PRIZE")]
        public NUMBER_VALUE_PRIZE? _NUMBER_VALUE_PRIZE { get; set; } = new NUMBER_VALUE_PRIZE();

        [XmlElement(ElementName = "FOLLOW_UP_CONTRACTS")]
        public FOLLOW_UP_CONTRACTS? _FOLLOW_UP_CONTRACTS { get; set; } = new FOLLOW_UP_CONTRACTS();

        [XmlElement(ElementName = "NO_DECISION_BINDING_CONTRACTING")]
        public NO_DECISION_BINDING_CONTRACTING? _NO_DECISION_BINDING_CONTRACTING { get; set; } = new NO_DECISION_BINDING_CONTRACTING();

        [XmlElement(ElementName = "CONTRACT_COVERED_GPA")]
        public CONTRACT_COVERED_GPA? _CONTRACT_COVERED_GPA { get; set; } = new CONTRACT_COVERED_GPA();

        [XmlElement(ElementName = "DURATION_TENDER_VALID")]
        public DURATION_TENDER_VALID? _DURATION_TENDER_VALID { get; set; } = new DURATION_TENDER_VALID();

        [XmlElement(ElementName = "FRAMEWORK")]
        public FRAMEWORK? _FRAMEWORK { get; set; } = new FRAMEWORK();

        [XmlElement(ElementName = "OPENING_CONDITION")]
        public OPENING_CONDITION? _OPENING_CONDITION { get; set; } = new OPENING_CONDITION();
    }

    #region [1] FORM_SECTION [2] F12_2014 [3] PROCEDURE
    [XmlRoot(ElementName = "PT_RESTRICTED")]
    public class PT_RESTRICTED
    {
        [XmlText]
        public string? Text { get; set; } = string.Empty;
    }

    [XmlRoot(ElementName = "NB_PARTICIPANTS")]
    public class NB_PARTICIPANTS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NB_PARTICIPANTS()
        {

        }
        public NB_PARTICIPANTS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NB_MIN_PARTICIPANTS")]
    public class NB_MIN_PARTICIPANTS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NB_MIN_PARTICIPANTS()
        {

        }
        public NB_MIN_PARTICIPANTS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NB_MAX_PARTICIPANTS")]
    public class NB_MAX_PARTICIPANTS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NB_MAX_PARTICIPANTS()
        {

        }
        public NB_MAX_PARTICIPANTS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "DATE_RECEIPT_TENDERS")]
    public class DATE_RECEIPT_TENDERS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public DATE_RECEIPT_TENDERS()
        {

        }
        public DATE_RECEIPT_TENDERS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "TIME_RECEIPT_TENDERS")]
    public class TIME_RECEIPT_TENDERS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public TIME_RECEIPT_TENDERS()
        {

        }
        public TIME_RECEIPT_TENDERS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = String.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "CRITERIA_EVALUATION")]
    public class CRITERIA_EVALUATION
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "LANGUAGES")]
    public class LANGUAGES
    {
        [XmlElement(ElementName = "LANGUAGE")]
        public List<LANGUAGE>? _LANGUAGE { get; set; } = new List<LANGUAGE>();
    }

    [XmlRoot(ElementName = "LANGUAGE")]
    public class LANGUAGE
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "VALUE")]
        public string? _VALUE { get; set; } = string.Empty;

        //ctor
        public LANGUAGE()
        {

        }
        public LANGUAGE(XElement root)
        {
            try
            {
                this._root = root;
                this._VALUE = root.Attribute("VALUE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._VALUE = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "PRIZE_AWARDED")]
    public class PRIZE_AWARDED
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public PRIZE_AWARDED()
        {

        }
        public PRIZE_AWARDED(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NUMBER_VALUE_PRIZE")]
    public class NUMBER_VALUE_PRIZE
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "FOLLOW_UP_CONTRACTS")]
    public class FOLLOW_UP_CONTRACTS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public FOLLOW_UP_CONTRACTS()
        {

        }
        public FOLLOW_UP_CONTRACTS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "NO_DECISION_BINDING_CONTRACTING")]
    public class NO_DECISION_BINDING_CONTRACTING
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        //ctor
        public NO_DECISION_BINDING_CONTRACTING()
        {

        }
        public NO_DECISION_BINDING_CONTRACTING(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "CONTRACT_COVERED_GPA")]
    public class CONTRACT_COVERED_GPA
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        public CONTRACT_COVERED_GPA()
        {

        }
        public CONTRACT_COVERED_GPA(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "DURATION_TENDER_VALID")]
    public class DURATION_TENDER_VALID
    {
        public XElement? _root { get; set; }

        [XmlAttribute(AttributeName = "TYPE")]
        public string? _TYPE { get; set; } = string.Empty;

        [XmlText]
        public string? _Text { get; set; } = string.Empty;

        public DURATION_TENDER_VALID()
        {

        }
        public DURATION_TENDER_VALID(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
                this._TYPE = root.Attribute("TYPE")!.Value;
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
                this._TYPE = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "FRAMEWORK")]
    public class FRAMEWORK
    {
        [XmlElement(ElementName = "SINGLE_OPERATOR")]
        public List<SINGLE_OPERATOR>? _SINGLE_OPERATOR { get; set; } = new List<SINGLE_OPERATOR>();
    }

    #region FRAMEWORK  
    [XmlRoot(ElementName = "SINGLE_OPERATOR")]
    public class SINGLE_OPERATOR
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;
        public SINGLE_OPERATOR()
        {

        }
        public SINGLE_OPERATOR(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }
    #endregion FRAMEWORK 

    [XmlRoot(ElementName = "OPENING_CONDITION")]
    public class OPENING_CONDITION
    {
        [XmlElement(ElementName = "DATE_OPENING_TENDERS")]
        public DATE_OPENING_TENDERS? _DATE_OPENING_TENDERS { get; set; } = new DATE_OPENING_TENDERS();

        [XmlElement(ElementName = "TIME_OPENING_TENDERS")]
        public TIME_OPENING_TENDERS? _TIME_OPENING_TENDERS { get; set; } = new TIME_OPENING_TENDERS();

        [XmlElement(ElementName = "PLACE")]
        public PLACE? _PLACE { get; set; } = new PLACE();

        [XmlElement(ElementName = "INFO_ADD")]
        public INFO_ADD? _INFO_ADD { get; set; } = new INFO_ADD();
    }

    #region OPENING_CONDITION
    [XmlRoot(ElementName = "DATE_OPENING_TENDERS")]
    public class DATE_OPENING_TENDERS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;
        public DATE_OPENING_TENDERS()
        {

        }
        public DATE_OPENING_TENDERS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "TIME_OPENING_TENDERS")]
    public class TIME_OPENING_TENDERS
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;
        public TIME_OPENING_TENDERS()
        {

        }
        public TIME_OPENING_TENDERS(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }

    [XmlRoot(ElementName = "PLACE")]
    public class PLACE
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "INFO_ADD")]
    public class INFO_ADD
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }
    #endregion OPENING_CONDITION

    #endregion [1] FORM_SECTION [2] F12_2014 [3] PROCEDURE

    [XmlRoot(ElementName = "COMPLEMENTARY_INFO")]
    public class COMPLEMENTARY_INFO
    {
        //elements
        [XmlElement(ElementName = "NO_RECURRENT_PROCUREMENT")]
        public string? _NO_RECURRENT_PROCUREMENT { get; set; } = string.Empty;

        [XmlElement(ElementName = "INFO_ADD")]
        public INFO_ADD? _INFO_ADD { get; set; } = new INFO_ADD();

        [XmlElement(ElementName = "ADDRESS_REVIEW_BODY")]
        public ADDRESS_REVIEW_BODY? _ADDRESS_REVIEW_BODY { get; set; } = new ADDRESS_REVIEW_BODY();

        [XmlElement(ElementName = "ADDRESS_MEDIATION_BODY")]
        public ADDRESS_MEDIATION_BODY? _ADDRESS_MEDIATION_BODY { get; set; } = new ADDRESS_MEDIATION_BODY();
        [XmlElement(ElementName = "ADDRESS_REVIEW_INFO")]
        public ADDRESS_REVIEW_INFO? _ADDRESS_REVIEW_INFO { get; set; } = new ADDRESS_REVIEW_INFO();

        [XmlElement(ElementName = "REVIEW_PROCEDURE")]
        public REVIEW_PROCEDURE? _REVIEW_PROCEDURE { get; set; } = new REVIEW_PROCEDURE();

        [XmlElement(ElementName = "DATE_DISPATCH_NOTICE")]
        public DATE_DISPATCH_NOTICE? _DATE_DISPATCH_NOTICE { get; set; } = new DATE_DISPATCH_NOTICE();
    }

    #region [1] FORM_SECTION [2] F12_2014 [3] COMPLEMENTARY_INFO
    [XmlRoot(ElementName = "ADDRESS_REVIEW_BODY")]
    public class ADDRESS_REVIEW_BODY
    {
        [XmlElement(ElementName = "OFFICIALNAME")]
        public OFFICIALNAME? _OFFICIALNAME { get; set; } = new OFFICIALNAME();

        [XmlElement(ElementName = "ADDRESS")]
        public ADDRESS? _ADDRESS { get; set; } = new ADDRESS();

        [XmlElement(ElementName = "TOWN")]
        public TOWN? _TOWN { get; set; } = new TOWN();

        [XmlElement(ElementName = "POSTAL_CODE")]
        public POSTAL_CODE? _POSTAL_CODE { get; set; } = new POSTAL_CODE();

        [XmlElement(ElementName = "COUNTRY")]
        public COUNTRY? _COUNTRY { get; set; } = new COUNTRY();

        [XmlElement(ElementName = "CONTACT_POINT")]
        public CONTACT_POINT? _CONTACT_POINT { get; set; } = new CONTACT_POINT();

        [XmlElement(ElementName = "PHONE")]
        public PHONE? _PHONE { get; set; } = new PHONE();

        [XmlElement(ElementName = "E_MAIL")]
        public E_MAIL? _E_MAIL { get; set; } = new E_MAIL();

        [XmlElement(ElementName = "FAX")]
        public FAX? _FAX { get; set; } = new FAX();

        [XmlElement(ElementName = "NUTS")]
        public NUTS? _NUTS { get; set; } = new NUTS();

        [XmlElement(ElementName = "URL_GENERAL")]
        public URL_GENERAL? _URL_GENERAL { get; set; } = new URL_GENERAL();

        [XmlElement(ElementName = "URL_BUYER")]
        public URL_BUYER? _URL_BUYER { get; set; } = new URL_BUYER();
    }

    [XmlRoot(ElementName = "ADDRESS_MEDIATION_BODY")]
    public class ADDRESS_MEDIATION_BODY
    {
        [XmlElement(ElementName = "OFFICIALNAME")]
        public OFFICIALNAME? _OFFICIALNAME { get; set; } = new OFFICIALNAME();

        [XmlElement(ElementName = "ADDRESS")]
        public ADDRESS? _ADDRESS { get; set; } = new ADDRESS();

        [XmlElement(ElementName = "TOWN")]
        public TOWN? _TOWN { get; set; } = new TOWN();

        [XmlElement(ElementName = "POSTAL_CODE")]
        public POSTAL_CODE? _POSTAL_CODE { get; set; } = new POSTAL_CODE();

        [XmlElement(ElementName = "COUNTRY")]
        public COUNTRY? _COUNTRY { get; set; } = new COUNTRY();

        [XmlElement(ElementName = "CONTACT_POINT")]
        public CONTACT_POINT? _CONTACT_POINT { get; set; } = new CONTACT_POINT();

        [XmlElement(ElementName = "PHONE")]
        public PHONE? _PHONE { get; set; } = new PHONE();

        [XmlElement(ElementName = "E_MAIL")]
        public E_MAIL? _E_MAIL { get; set; } = new E_MAIL();

        [XmlElement(ElementName = "FAX")]
        public FAX? _FAX { get; set; } = new FAX();

        [XmlElement(ElementName = "NUTS")]
        public NUTS? _NUTS { get; set; } = new NUTS();

        [XmlElement(ElementName = "URL_GENERAL")]
        public URL_GENERAL? _URL_GENERAL { get; set; } = new URL_GENERAL();

        [XmlElement(ElementName = "URL_BUYER")]
        public URL_BUYER? _URL_BUYER { get; set; } = new URL_BUYER();
    }

    [XmlRoot(ElementName = "ADDRESS_REVIEW_INFO")]
    public class ADDRESS_REVIEW_INFO
    {
        [XmlElement(ElementName = "OFFICIALNAME")]
        public OFFICIALNAME? _OFFICIALNAME { get; set; } = new OFFICIALNAME();

        [XmlElement(ElementName = "ADDRESS")]
        public ADDRESS? _ADDRESS { get; set; } = new ADDRESS();

        [XmlElement(ElementName = "TOWN")]
        public TOWN? _TOWN { get; set; } = new TOWN();

        [XmlElement(ElementName = "POSTAL_CODE")]
        public POSTAL_CODE? _POSTAL_CODE { get; set; } = new POSTAL_CODE();

        [XmlElement(ElementName = "COUNTRY")]
        public COUNTRY? _COUNTRY { get; set; } = new COUNTRY();

        [XmlElement(ElementName = "CONTACT_POINT")]
        public CONTACT_POINT? _CONTACT_POINT { get; set; } = new CONTACT_POINT();

        [XmlElement(ElementName = "PHONE")]
        public PHONE? _PHONE { get; set; } = new PHONE();

        [XmlElement(ElementName = "E_MAIL")]
        public E_MAIL? _E_MAIL { get; set; } = new E_MAIL();

        [XmlElement(ElementName = "FAX")]
        public FAX? _FAX { get; set; } = new FAX();

        [XmlElement(ElementName = "NUTS")]
        public NUTS? _NUTS { get; set; } = new NUTS();

        [XmlElement(ElementName = "URL_GENERAL")]
        public URL_GENERAL? _URL_GENERAL { get; set; } = new URL_GENERAL();

        [XmlElement(ElementName = "URL_BUYER")]
        public URL_BUYER? _URL_BUYER { get; set; } = new URL_BUYER();
    }

    [XmlRoot(ElementName = "REVIEW_PROCEDURE")]
    public class REVIEW_PROCEDURE
    {
        [XmlElement(ElementName = "P")]
        public List<P>? _P { get; set; } = new List<P>();
    }

    [XmlRoot(ElementName = "DATE_DISPATCH_NOTICE")]
    public class DATE_DISPATCH_NOTICE
    {
        public XElement? _root { get; set; }

        [XmlText]
        public string? _Text { get; set; } = string.Empty;
        public DATE_DISPATCH_NOTICE()
        {

        }
        public DATE_DISPATCH_NOTICE(XElement root)
        {
            try
            {
                this._root = root;
                this._Text = root.Value.ToString();
            }
            catch (NullReferenceException)
            {
                this._root = null;
                this._Text = string.Empty;
            }
        }
    }
    #endregion [1] FORM_SECTION [2] F12_2014 [3] COMPLEMENTARY_INFO

    #endregion [1] FORM_SECTION [2] F

    #endregion [1] FORM_SECTION






}

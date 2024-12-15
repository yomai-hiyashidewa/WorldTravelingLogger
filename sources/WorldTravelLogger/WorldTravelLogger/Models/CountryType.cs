using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    //ISO 3166-1 
    public enum CountryType
    {
        [Display(Name = "Japan")]
        JPN,    // 	Japan
        
        // East Asia
        [Display(Name = "South Korea")]
        KOR,    // Korea (the Republic of)
        [Display(Name = "TTaiwan")]
        TWN,    // TTaiwan, Province of China
        [Display(Name = "Hong Kong")]
        HKG,    // Hong Kong
        [Display(Name = "Philippines")]
        PHL,    // Philippines
        [Display(Name = "Viet Nam")]
        VNM,    // Viet Nam
        [Display(Name = "Malaysia")]
        MYS,    // Malaysia

        // oceania
        [Display(Name = "Australia")]
        AUS,    // Australia
        
        // north america
        [Display(Name = "United States of America")]
        USA,    // United States of America
        [Display(Name = "Canada")]
        CAN,    // Canada

        // central america
        [Display(Name = "Mexico")]
        MEX,    // Mexico
        [Display(Name = "Guatemala")]
        GTM,    // Guatemala
        [Display(Name = "Belize")]
        BLZ,    // Belize
        [Display(Name = "El Salvador")]
        SLV,    // El Salvador
        [Display(Name = "Honduras")]
        HND,    // Honduras
        [Display(Name = "Nicaragua")]
        NIC,    // Nicaragua
        [Display(Name = "Costa Rica")]
        CRI,    // Costa Rica
        [Display(Name = "Panama")]
        PAN,    // Panama
        [Display(Name = "Cuba")]
        CUB,    // Cuba
        // south america
        [Display(Name = "Colombia")]
        COL,    // Colombia
        [Display(Name = "Ecuador")]
        ECU,    // Ecuador
        [Display(Name = "Pérou")]
        PER,    // Pérou
        [Display(Name = "Bolivia")]
        BOL,    // Bolivia (Plurinational State of)
        [Display(Name = "Chile")]
        CHL,    // Chile
        [Display(Name = "Paraguay")]
        PRY,    // Paraguay
        [Display(Name = "Argentina")]
        ARG,    // Argentina
        [Display(Name = "Uruguay")]
        URY,    // Uruguay
        [Display(Name = "Brazil")]
        BRA,    // Brazil

        
        // Europe
        [Display(Name = "Iceland")]
        ISL,    // Iceland
        // UK
        [Display(Name = "Ireland")]
        IRL,    // Ireland
        [Display(Name = "UK")]
        GBR,    // United Kingdom of Great Britain and Northern Ireland
        // north Europe
        [Display(Name = "Norway")]
        NOR,    // Norway
        [Display(Name = "Sweden")]
        SWE,    // Sweden
        [Display(Name = "Finland")]
        FIN,    // Finland
        [Display(Name = "Denmark")]
        DNK,    // Denmark
                // west Europe
        [Display(Name = "Portugal")]
        PRT,    // Portugal
        [Display(Name = "Spain")]
        ESP,    // Spain
        [Display(Name = "Andorra")]
        AND,    // Andorra
        [Display(Name = "France")]
        FRA,    // France
        [Display(Name = "Monaco")]
        MCO,    // Monaco
        [Display(Name = "Belgium")]
        BEL,    // Belgium
        [Display(Name = "Luxembourg")]
        LUX,    // Luxembourg
        [Display(Name = "Netherlands")]
        NLD,    // Netherlands
        [Display(Name = "Germany")]
        DEU,    // Germany
        [Display(Name = "Switzerland")]
        CHE,    // Switzerland
        [Display(Name = "Liechtenstein")]
        LIE,    // Liechtenstein
        [Display(Name = "Austria")]
        AUT,    // Austria
        [Display(Name = "Hungary")]
        HUN,    // Hungary
        [Display(Name = "Czechia")]
        CZE,    // Czechia
        [Display(Name = "Slovakia")]
        SVK,    // Slovakia
        [Display(Name = "Poland")]
        POL,    // Poland
        [Display(Name = "Italy")]
        ITA,    // Italy
        [Display(Name = "Knights of Malta")]
        MOM,    // Malta Knight
        [Display(Name = "Vatican City")]
        VAT,    // Holy See
        [Display(Name = "San Marino")]
        SMR,    // San Marino
        [Display(Name = "Estonia")]
        EST,    // Estonia
        [Display(Name = "Latvia")]
        LVA,    // Latvia
        [Display(Name = "Lithuania")]
        LTU,    // Lithuania
        [Display(Name = "Croatia")]
        HRV,    // Croatia
        [Display(Name = "Slovenia")]
        SVN,    // Slovenia
        [Display(Name = "Bosnia and Herzegovina")]
        BIH,    // Bosnia and Herzegovina
        [Display(Name = "Serbia")]
        SRB,    // Serbia
        [Display(Name = "Kosovo")]
        KVX,    // Kosovo
        [Display(Name = "Montenegro")]
        MNE,    // Montenegro
        [Display(Name = "North Macedonia")]
        MKD,    // North Macedonia
        [Display(Name = "Bulgaria")]
        BGR,    // Bulgaria
        [Display(Name = "Romania")]
        ROU,    // Romania
        [Display(Name = "Moldova")]
        MDA,    // Moldova, Republic of
        [Display(Name = "Albania")]
        ALB,    // Albania
        [Display(Name = "Greece")]
        GRC,    // Greece
       
        [Display(Name = "Cyprus")]
        CYP,    // Cyprus
        [Display(Name = "Malta")]
        MLT,    // Malta
        // north africa
        [Display(Name = "Egypt")]
        EGY,    // Egypt
        [Display(Name = "Tunisia")]
        TUN,    // Tunisia
        [Display(Name = "Morocco")]
        MAR,    // Morocco
                // Central Asia
        [Display(Name = "Turkiye")]
        TUR,    // Turkiye
        [Display(Name = "Qatar")]
        QAT,    // Qatar
        [Display(Name = "United Arab Emirates")]
        ARE,    // United Arab Emirates
        [Display(Name = "Georgia")]
        GEO,    // Georgia
        [Display(Name = "Armenia")]
        ARM,    // Armenia
        [Display(Name = "Uzbekistan")]
        UZB,    // Uzbekistan
        // South Asia
        [Display(Name = "India")]
        IND,    // India
        // not yet
        AZE,    // Azerbaijan
        AFG,    // Afghanistan
        DZA,    // Algeria
        ABW,    // Aruba
        
        AIA,    // Anguilla
        AGO,    // Angola
        ATG,    // Antigua and Barbuda
        YEM,    // Yemen
        ISR,    // Israel
        IRQ,    // Iraq
        IRN,    // Iran (Islamic Republic of)
        IDN,    // Indonesia
        WLF,    // Wallis and Futuna
        UGA,    // Uganda
        UKR,    // Ukraine
        SWZ,    // Eswatini
        ETH,    // Ethiopia
        ERI,    // Eritrea
        OMN,    // Oman
        GHA,    // Ghana
        CPV,    // Cabo Verde
        GUY,    // Guyana
        KAZ,    // Kazakhstan
        GAB,    // Gabon
        CMR,    // Cameroon
        GMB,    // Gambia
        KHM,    // Cambodia
        GIN,    // Guinea
        GNB,    // Guinea-Bissau
        CUW,    // Curaçao
        KIR,    // Kiribati
        KGZ,    // Kyrgyzstan
        KWT,    // Kuwait
        GRD,    // Grenada
        KEN,    // Kenya
        CIV,    // Côte d'Ivoire
        COM,    // Comoros
        COG,    // Congo
        COD,    // Congo, Democratic Republic of the
        SAU,    // Saudi Arabia
        WSM,    // Samoa
        ZMB,    // Zambia
        SLE,    // Sierra Leone
        DJI,    // 	Djibouti
        JAM,    // Jamaica
        SYR,    // Syrian Arab Republic
        SGP,    // Singapore
        ZWE,    // Zimbabwe
        SDN,    // Sudan
        SUR,    // Suriname
        LKA,    // Sri Lanka
        SYC,    // Seychelles
        SEN,    // Senegal
        LCA,    // Saint Lucia
        SOM,    // Somalia
        THA,    // Thailand
        TJK,    // Tajikistan
        TZA,    // Tanzania, United Republic of
        TCD,    // Chad
        CAF,    // Central African Republic
        CHN,    // China
        PRK,    // Korea (the Democratic People's Republic of)
        TUV,    // Tuvalu
        TGO,    // Togo
        TKL,    // Tokelau
        DOM,    // Dominican Republic
        DMA,    // Dominica
        TTO,    // Trinidad and Tobago
        TKM,    // Turkmenistan
        TON,    // Tonga
        NGA,    // Nigeria
        NRU,    // Nauru
        NAM,    // Namibia
        ATA,    // Antarctica
        NIU,    // Niue
        NER,    // Niger
        ESH,    // Western Sahara
        NCL,    // New Caledonia
        NZL,    // New Zealand
        NPL,    // Népal
        BHR,    // Bahrain
        HTI,    // Haiti
        PAK,    // Pakistan
       
        VUT,    // Vanuatu
        BHS,    // Bahamas
        PNG,    // Papua New Guinea
        BMU,    // Bermuda
        PLW,    // Palau
        BRB,    // Barbados
        PSE,    // Palestine, State of	
        BGD,    // Bangladesh
        TLS,    // Timor-Leste
        PCN,    // Pitcairn
        FJI,    // Fiji
        BTN,    // Bhutan
        PRI,    // Puerto Rico
        FRO,    // Faroe Islands
        BFA,    // Burkina Faso
        BRN,    // Brunei Darussalam
        BDI,    // Burundi
        BEN,    // Benin
        VEN,    // Venezuela (Bolivarian Republic of)
        BLR,    // Belarus
        BWA,    // Botswana
        MAC,    // Macau
        MDG,    // Madagascar
        MYT,    // Mayotte
        MWI,    // Malawi
        MLI,    // Mali
        FSM,    // Micronesia (Federated States of)
        ZAF,    // South Africa
        SSD,    // South Sudan
        MMR,    // Myanmar
        MUS,    // Mauritius
        MRT,    // Mauritania
        MOZ,    // Mozambique
        MDV,    // Maldives
        MNG,    // Mongolia
        JOR,    // Jordan
        LAO,    // Lao People's Democratic Republic
        LBY,    // Libya
        LBR,    // Liberia
        RWA,    // Rwanda
        LSO,    // Lesotho
        LBN,    // Lebanon
        RUS,    // Russian Federation
        UNK,    // unknown













    }
}

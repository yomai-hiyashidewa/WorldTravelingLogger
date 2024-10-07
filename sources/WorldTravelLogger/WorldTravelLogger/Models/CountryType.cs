using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models
{
    //ISO 3166-1 
    public enum CountryType
    {
        JPN,    // 	Japan
        ISL,    // Iceland
        IRL,    // Ireland
        AZE,    // Azerbaijan
        AFG,    // Afghanistan
        USA,    // United States of America
        ARE,    // United Arab Emirates
        DZA,    // Algeria
        ARG,    // Argentina
        ABW,    // Aruba
        ALB,    // Albania
        ARM,    // Armenia
        AIA,    // Anguilla
        AGO,    // Angola
        ATG,    // Antigua and Barbuda
        AND,    // Andorra
        YEM,    // Yemen
        GBR,    // United Kingdom of Great Britain and Northern Ireland
        ISR,    // Israel
        ITA,    // Italy
        IRQ,    // Iraq
        IRN,    // Iran (Islamic Republic of)
        IND,    // India
        IDN,    // Indonesia
        WLF,    // Wallis and Futuna
        UGA,    // Uganda
        UKR,    // Ukraine
        UZB,    // Uzbekistan
        URY,    // Uruguay
        ECU,    // Ecuador
        EGY,    // Egypt
        EST,    // Estonia
        SWZ,    // Eswatini
        ETH,    // Ethiopia
        ERI,    // Eritrea
        SLV,    // El Salvador
        AUS,    // Australia
        AUT,    // Austria
        OMN,    // Oman
        NLD,    // Netherlands
        GHA,    // Ghana
        CPV,    // Cabo Verde
        GUY,    // Guyana
        KAZ,    // Kazakhstan
        QAT,    // Qatar
        CAN,    // Canada
        GAB,    // Gabon
        CMR,    // Cameroon
        GMB,    // Gambia
        KHM,    // Cambodia
        MKD,    // North Macedonia
        GIN,    // Guinea
        GNB,    // Guinea-Bissau
        CYP,    // Cyprus
        CUB,    // Cuba
        CUW,    // Curaçao
        GRC,    // Greece
        KIR,    // Kiribati
        KGZ,    // Kyrgyzstan
        GTM,    // Guatemala
        KWT,    // Kuwait
        GRD,    // Grenada
        HRV,    // Croatia
        KEN,    // Kenya
        CIV,    // Côte d'Ivoire
        CRI,    // Costa Rica
        COM,    // Comoros
        COL,    // Colombia
        COG,    // Congo
        COD,    // Congo, Democratic Republic of the
        SAU,    // Saudi Arabia
        WSM,    // Samoa
        ZMB,    // Zambia
        SMR,    // San Marino
        SLE,    // Sierra Leone
        DJI,    // 	Djibouti
        JAM,    // Jamaica
        GEO,    // Georgia
        SYR,    // Syrian Arab Republic
        SGP,    // Singapore
        ZWE,    // Zimbabwe
        CHE,    // Switzerland
        SWE,    // Sweden
        SDN,    // Sudan
        ESP,    // Spain
        SUR,    // Suriname
        LKA,    // Sri Lanka
        SVK,    // Slovakia
        SVN,    // Slovenia
        SYC,    // Seychelles
        SEN,    // Senegal
        SRB,    // Serbia
        LCA,    // Saint Lucia
        SOM,    // Somalia
        THA,    // Thailand
        KOR,    // Korea (the Republic of)
        TWN,    // TTaiwan, Province of China
        TJK,    // Tajikistan
        TZA,    // Tanzania, United Republic of
        CZE,    // Czechia
        TCD,    // Chad
        CAF,    // Central African Republic
        CHN,    // China
        TUN,    // Tunisia
        PRK,    // Korea (the Democratic People's Republic of)
        CHL,    // Chile
        TUV,    // Tuvalu
        DNK,    // Denmark
        DEU,    // Germany
        TGO,    // Togo
        TKL,    // Tokelau
        DOM,    // Dominican Republic
        DMA,    // Dominica
        TTO,    // Trinidad and Tobago
        TKM,    // Turkmenistan
        TUR,    // Turkiye
        TON,    // Tonga
        NGA,    // Nigeria
        NRU,    // Nauru
        NAM,    // Namibia
        ATA,    // Antarctica
        NIU,    // Niue
        NIC,    // Nicaragua
        NER,    // Niger
        ESH,    // Western Sahara
        NCL,    // New Caledonia
        NZL,    // New Zealand
        NPL,    // Népal
        NOR,    // Norway
        BHR,    // Bahrain
        HTI,    // Haiti
        PAK,    // Pakistan
        VAT,    // Holy See
        PAN,    // Panama
        VUT,    // Vanuatu
        BHS,    // Bahamas
        PNG,    // Papua New Guinea
        BMU,    // Bermuda
        PLW,    // Palau
        PRY,    // Paraguay
        BRB,    // Barbados
        PSE,    // Palestine, State of	
        HUN,    // Hungary
        BGD,    // Bangladesh
        TLS,    // Timor-Leste
        PCN,    // Pitcairn
        FJI,    // Fiji
        PHL,    // Philippines
        FIN,    // Finland
        BTN,    // Bhutan
        PRI,    // Puerto Rico
        FRO,    // Faroe Islands
        BRA,    // Brazil
        FRA,    // France
        BGR,    // Bulgaria
        BFA,    // Burkina Faso
        BRN,    // Brunei Darussalam
        BDI,    // Burundi
        VNM,    // Viet Nam
        BEN,    // Benin
        VEN,    // Venezuela (Bolivarian Republic of)
        BLR,    // Belarus
        BLZ,    // Belize
        PER,    // Pérou
        BEL,    // Belgium
        POL,    // Poland
        BIH,    // Bosnia and Herzegovina
        BWA,    // Botswana
        BOL,    // Bolivia (Plurinational State of)
        PRT,    // Portugal
        HKG,    // Hong Kong
        HND,    // Honduras
        MAC,    // Macau
        MDG,    // Madagascar
        MYT,    // Mayotte
        MWI,    // Malawi
        MLI,    // Mali
        MLT,    // Malta
        MYS,    // Malaysia
        FSM,    // Micronesia (Federated States of)
        ZAF,    // South Africa
        SSD,    // South Sudan
        MMR,    // Myanmar
        MEX,    // Mexico
        MUS,    // Mauritius
        MRT,    // Mauritania
        MOZ,    // Mozambique
        MCO,    // Monaco
        MDV,    // Maldives
        MDA,    // Moldova, Republic of
        MAR,    // Morocco
        MNG,    // Mongolia
        MNE,    // Montenegro
        JOR,    // Jordan
        LAO,    // Lao People's Democratic Republic
        LVA,    // Latvia
        LTU,    // Lithuania
        LBY,    // Libya
        LIE,    // Liechtenstein
        LBR,    // Liberia
        ROU,    // Romania
        LUX,    // Luxembourg
        RWA,    // Rwanda
        LSO,    // Lesotho
        LBN,    // Lebanon
        RUS,    // Russian Federation
        KVX,    // Kosovo
        UNK,    // unknown













    }
}

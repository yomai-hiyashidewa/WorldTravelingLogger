using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTravelLogger.Models.Enumeration
{
    // ISO4217
    // https://ja.wikipedia.org/wiki/ISO_4217
    public enum CurrencyType
    {
        [Display(Name = "yen")]
        JPY,    // 日本円
        [Display(Name = "Euro")]
        EUR,    // ユーロ
        // ドル
        [Display(Name = "US dollar")]
        USD,    // アメリカドル
        [Display(Name = "A dollar")]
        AUD,    // オーストラリアドル
        [Display(Name = "C dollar")]
        CAD,    // カナダドル
        [Display(Name = "B dollar")]
        BZD,    // ベリーズドル
        [Display(Name = "NT dollar")]
        TWD,    // 新台湾ドル
        [Display(Name = "HK dollar")]
        HKD,    // 香港ドル
        // ペソ
        [Display(Name = "M peso")]
        MXP,    // メキシコペソ
        [Display(Name = "C peso")]
        CUP,    // キューバペソ
        [Display(Name = "Arpeso")]
        ARS,    // アルゼンチンペソ
        [Display(Name = "U peso")]
        UYU,    // ウルグアイペソ
        [Display(Name = "CO peso")]
        COP,    // コロンビアペソ
        [Display(Name = "CL peso")]
        CLP,    // チリペソ
        [Display(Name = "P peso")]
        PHP,    // フィリピンペソ

        // ポンド
        [Display(Name = "UK Pound")]
        GBP,    // イギリスポンド
        [Display(Name = "E pound")]
        EGP,    // エジプトポンド

        // ディルハム
        [Display(Name = "UAE dirham")]
        AED,    // UAEディルハム
        [Display(Name = "M dirham")]
        MAD,    // モロッコディルハム

        // クローナ
        [Display(Name = "D krone")]
        DKK,    // デンマーククローネ
        [Display(Name = "I króna")]
        ISK,    // アイスランドクローナ
        [Display(Name = "S krona ")]
        SEK,    // スウェーデンクローナ
        [Display(Name = "N krone")]
        NOK,    // ノルウェークローネ

        // ディナール
        [Display(Name = "S dinar")]
        RSD,    // セルビアディナール
        [Display(Name = "T dinar")]
        TND,    // チュニジアディナール
        [Display(Name = "M denar")]
        MKD,    // マケドニア・デナール

        // レイ
        [Display(Name = "M leu")]
        MDL,    // モルドバレイ
        [Display(Name = "R leu")]
        RON,    // ルーマニアレイ


        [Display(Name = "lek")]
        ALL,    // アルバニアレク
        [Display(Name = "dram")]
        AMD,    // アルメニアドラム
       
        [Display(Name = "rupee")]
        INR,    // インドルピー
        [Display(Name = "U sum")]
        UZS,    // ウズベキスタンスム
       
       
        [Display(Name = "quetzal")]
        GTQ,    // グアテマラケツァル
       
        [Display(Name = "colon")]
        CRC,    // コスタリカコロン
       
        [Display(Name = "lari")]
        GEL,    // ジョージアラリ
        [Display(Name = "franc")]
        CHF,    // スイスフラン
        
        
        [Display(Name = "koruna")]
        CZK,    // チェココルナ
      
        
        [Display(Name = "lira")]
        TRY,    // トルコリラ
        [Display(Name = "córdoba")]
        NIO,    // ニカラグアコルドバオロ
        [Display(Name = "forint")]
        HUF,    // ハンガリーフォリント
        [Display(Name = "guaraní")]
        PYG,    // パラグアイグアラニ
        
        [Display(Name = "real")]
        BRL,    // ブラジルレアル
        [Display(Name = "lev")]
        BGN,    // ブルガリアレフ
        [Display(Name = "đồng")]
        VND,    // ベトナムドン
        [Display(Name = "sol")]
        PEN,    // ペルーソル
        [Display(Name = "BH mark")]
        BAM,    // ボスニア・ヘルツェゴビナマルク
        [Display(Name = "Boliviano")]
        BOB,    // ボリビアボリビアノ
        [Display(Name = "złoty")]
        PLN,    // ポーランドズロチ
       
       
       
        [Display(Name = "won")]
        KRW,    // 大韓民国ウォン
       

    }

    public enum MajorCurrencytype
    {

        [Display(Name = "円")]
        JPN,    // 日本円
        [Display(Name = "$")]
        USD,    // USドル
        [Display(Name = "€")]
        EUR,    // ユーロ
    }
}

using FEZAutoScore.Extension;
using FEZAutoScore.Model.Entity;
using FEZAutoScore.Properties;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FEZAutoScore.Model.Analyzer.Ocr
{
    public class SkillOcr : StringOcr
    {
        private Dictionary<string, BitArray> _skillDicionary;

        public SkillOcr()
        {
            _skillDicionary = new Dictionary<string, BitArray>()
            {
                { nameof(Resources.Cestus_アースバインド), Resources.Cestus_アースバインド.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_アースバインド_S), Resources.Cestus_アースバインド_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_インテンスファイ), Resources.Cestus_インテンスファイ.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_インテンスファイ_S), Resources.Cestus_インテンスファイ_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_エナジースフィア), Resources.Cestus_エナジースフィア.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_エナジースフィア_S), Resources.Cestus_エナジースフィア_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_エンダーレイド), Resources.Cestus_エンダーレイド.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_エンダーレイド_S), Resources.Cestus_エンダーレイド_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ゲイザースマッシュ), Resources.Cestus_ゲイザースマッシュ.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ゲイザースマッシュ_S), Resources.Cestus_ゲイザースマッシュ_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_サイクロンディザスター), Resources.Cestus_サイクロンディザスター.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_サイクロンディザスター_S), Resources.Cestus_サイクロンディザスター_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_シャットアウト), Resources.Cestus_シャットアウト.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_シャットアウト_S), Resources.Cestus_シャットアウト_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ショックウェイブ), Resources.Cestus_ショックウェイブ.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ショックウェイブ_S), Resources.Cestus_ショックウェイブ_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ソリッドストライク), Resources.Cestus_ソリッドストライク.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ソリッドストライク_S), Resources.Cestus_ソリッドストライク_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_タワードミネーション), Resources.Cestus_タワードミネーション.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_タワードミネーション_S), Resources.Cestus_タワードミネーション_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ドレインクロー), Resources.Cestus_ドレインクロー.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ドレインクロー_S), Resources.Cestus_ドレインクロー_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ノックインパクト), Resources.Cestus_ノックインパクト.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ノックインパクト_S), Resources.Cestus_ノックインパクト_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ハードレインフォース), Resources.Cestus_ハードレインフォース.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ハードレインフォース_S), Resources.Cestus_ハードレインフォース_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ハームアクティベイト), Resources.Cestus_ハームアクティベイト.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ハームアクティベイト_S), Resources.Cestus_ハームアクティベイト_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ホーネットスティング), Resources.Cestus_ホーネットスティング.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_ホーネットスティング_S), Resources.Cestus_ホーネットスティング_S.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_通常攻撃), Resources.Cestus_通常攻撃.GenerateDifferenceHash() },
                { nameof(Resources.Cestus_通常攻撃_S), Resources.Cestus_通常攻撃_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_アクセラレーション), Resources.Fencer_アクセラレーション.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_アクセラレーション_S), Resources.Fencer_アクセラレーション_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_イレイスマジック), Resources.Fencer_イレイスマジック.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_イレイスマジック_S), Resources.Fencer_イレイスマジック_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_エリアルフォール), Resources.Fencer_エリアルフォール.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_エリアルフォール_S), Resources.Fencer_エリアルフォール_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_オブティンプロテクト), Resources.Fencer_オブティンプロテクト.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_オブティンプロテクト_S), Resources.Fencer_オブティンプロテクト_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_シャイニングクロス), Resources.Fencer_シャイニングクロス.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_シャイニングクロス_S), Resources.Fencer_シャイニングクロス_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_ストライクダウン), Resources.Fencer_ストライクダウン.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_ストライクダウン_S), Resources.Fencer_ストライクダウン_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_テンペストピアス), Resources.Fencer_テンペストピアス.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_テンペストピアス_S), Resources.Fencer_テンペストピアス_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_デュアルストライク), Resources.Fencer_デュアルストライク.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_デュアルストライク_S), Resources.Fencer_デュアルストライク_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_フィニッシュスラスト), Resources.Fencer_フィニッシュスラスト.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_フィニッシュスラスト_S), Resources.Fencer_フィニッシュスラスト_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_フラッシュスティンガー), Resources.Fencer_フラッシュスティンガー.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_フラッシュスティンガー_S), Resources.Fencer_フラッシュスティンガー_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_ペネトレイトスラスト), Resources.Fencer_ペネトレイトスラスト.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_ペネトレイトスラスト_S), Resources.Fencer_ペネトレイトスラスト_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_ラピッドファンデヴ), Resources.Fencer_ラピッドファンデヴ.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_ラピッドファンデヴ_S), Resources.Fencer_ラピッドファンデヴ_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_リバースキック), Resources.Fencer_リバースキック.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_リバースキック_S), Resources.Fencer_リバースキック_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_ヴィガーエイド), Resources.Fencer_ヴィガーエイド.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_ヴィガーエイド_S), Resources.Fencer_ヴィガーエイド_S.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_通常攻撃), Resources.Fencer_通常攻撃.GenerateDifferenceHash() },
                { nameof(Resources.Fencer_通常攻撃_S), Resources.Fencer_通常攻撃_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_アローレイン), Resources.Scout_アローレイン.GenerateDifferenceHash() },
                { nameof(Resources.Scout_アローレイン_S), Resources.Scout_アローレイン_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_アームブレイク), Resources.Scout_アームブレイク.GenerateDifferenceHash() },
                { nameof(Resources.Scout_アームブレイク_S), Resources.Scout_アームブレイク_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_イーグルショット), Resources.Scout_イーグルショット.GenerateDifferenceHash() },
                { nameof(Resources.Scout_イーグルショット_S), Resources.Scout_イーグルショット_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_エアレイド), Resources.Scout_エアレイド.GenerateDifferenceHash() },
                { nameof(Resources.Scout_エアレイド_S), Resources.Scout_エアレイド_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ガードブレイク), Resources.Scout_ガードブレイク.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ガードブレイク_S), Resources.Scout_ガードブレイク_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_クイックビート), Resources.Scout_クイックビート.GenerateDifferenceHash() },
                { nameof(Resources.Scout_クイックビート_S), Resources.Scout_クイックビート_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_クラッシュショット), Resources.Scout_クラッシュショット.GenerateDifferenceHash() },
                { nameof(Resources.Scout_クラッシュショット_S), Resources.Scout_クラッシュショット_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_コメットキャノン), Resources.Scout_コメットキャノン.GenerateDifferenceHash() },
                { nameof(Resources.Scout_コメットキャノン_S), Resources.Scout_コメットキャノン_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_スウィープキャノン), Resources.Scout_スウィープキャノン.GenerateDifferenceHash() },
                { nameof(Resources.Scout_スウィープキャノン_S), Resources.Scout_スウィープキャノン_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_スパイダーウェブ), Resources.Scout_スパイダーウェブ.GenerateDifferenceHash() },
                { nameof(Resources.Scout_スパイダーウェブ_S), Resources.Scout_スパイダーウェブ_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ダガーストライク), Resources.Scout_ダガーストライク.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ダガーストライク_S), Resources.Scout_ダガーストライク_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_トゥルーショット), Resources.Scout_トゥルーショット.GenerateDifferenceHash() },
                { nameof(Resources.Scout_トゥルーショット_S), Resources.Scout_トゥルーショット_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ドッジシュート), Resources.Scout_ドッジシュート.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ドッジシュート_S), Resources.Scout_ドッジシュート_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ハイド), Resources.Scout_ハイド.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ハイド_S), Resources.Scout_ハイド_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_バーストキャノン), Resources.Scout_バーストキャノン.GenerateDifferenceHash() },
                { nameof(Resources.Scout_バーストキャノン_S), Resources.Scout_バーストキャノン_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_パニッシングストライク), Resources.Scout_パニッシングストライク.GenerateDifferenceHash() },
                { nameof(Resources.Scout_パニッシングストライク_D), Resources.Scout_パニッシングストライク_D.GenerateDifferenceHash() },
                { nameof(Resources.Scout_パニッシングストライク_S), Resources.Scout_パニッシングストライク_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_パワーシュート), Resources.Scout_パワーシュート.GenerateDifferenceHash() },
                { nameof(Resources.Scout_パワーシュート_S), Resources.Scout_パワーシュート_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_パワーブレイク), Resources.Scout_パワーブレイク.GenerateDifferenceHash() },
                { nameof(Resources.Scout_パワーブレイク_S), Resources.Scout_パワーブレイク_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ピアッシングシュート), Resources.Scout_ピアッシングシュート.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ピアッシングシュート_S), Resources.Scout_ピアッシングシュート_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ファストショット), Resources.Scout_ファストショット.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ファストショット_S), Resources.Scout_ファストショット_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_フリックショット), Resources.Scout_フリックショット.GenerateDifferenceHash() },
                { nameof(Resources.Scout_フリックショット_S), Resources.Scout_フリックショット_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ブレイズショット), Resources.Scout_ブレイズショット.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ブレイズショット_S), Resources.Scout_ブレイズショット_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ホワイトバレット), Resources.Scout_ホワイトバレット.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ホワイトバレット_S), Resources.Scout_ホワイトバレット_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ポイズンショット), Resources.Scout_ポイズンショット.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ポイズンショット_S), Resources.Scout_ポイズンショット_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ポイズンブロウ), Resources.Scout_ポイズンブロウ.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ポイズンブロウ_S), Resources.Scout_ポイズンブロウ_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ラッシュバレット), Resources.Scout_ラッシュバレット.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ラッシュバレット_S), Resources.Scout_ラッシュバレット_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_レッグブレイク), Resources.Scout_レッグブレイク.GenerateDifferenceHash() },
                { nameof(Resources.Scout_レッグブレイク_S), Resources.Scout_レッグブレイク_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ヴァイパーバイト), Resources.Scout_ヴァイパーバイト.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ヴァイパーバイト_S), Resources.Scout_ヴァイパーバイト_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ヴォイドダークネス), Resources.Scout_ヴォイドダークネス.GenerateDifferenceHash() },
                { nameof(Resources.Scout_ヴォイドダークネス_S), Resources.Scout_ヴォイドダークネス_S.GenerateDifferenceHash() },
                { nameof(Resources.Scout_通常攻撃), Resources.Scout_通常攻撃.GenerateDifferenceHash() },
                { nameof(Resources.Scout_通常攻撃_S), Resources.Scout_通常攻撃_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_アイスジャベリン), Resources.Sorcerer_アイスジャベリン.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_アイスジャベリン_D), Resources.Sorcerer_アイスジャベリン_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_アイスジャベリン_S), Resources.Sorcerer_アイスジャベリン_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_アイスターゲット), Resources.Sorcerer_アイスターゲット.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_アイスターゲット_D), Resources.Sorcerer_アイスターゲット_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_アイスターゲット_S), Resources.Sorcerer_アイスターゲット_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_アイスボルト), Resources.Sorcerer_アイスボルト.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_アイスボルト_S), Resources.Sorcerer_アイスボルト_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_エレキドライブ), Resources.Sorcerer_エレキドライブ.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_エレキドライブ_D), Resources.Sorcerer_エレキドライブ_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_エレキドライブ_S), Resources.Sorcerer_エレキドライブ_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_グラビティキャプチャー), Resources.Sorcerer_グラビティキャプチャー.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_グラビティキャプチャー_D), Resources.Sorcerer_グラビティキャプチャー_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_グラビティキャプチャー_S), Resources.Sorcerer_グラビティキャプチャー_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_サンダーボルト), Resources.Sorcerer_サンダーボルト.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_サンダーボルト_D), Resources.Sorcerer_サンダーボルト_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_サンダーボルト_S), Resources.Sorcerer_サンダーボルト_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ジャッジメントレイ), Resources.Sorcerer_ジャッジメントレイ.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ジャッジメントレイ_D), Resources.Sorcerer_ジャッジメントレイ_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ジャッジメントレイ_S), Resources.Sorcerer_ジャッジメントレイ_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_スパークフレア), Resources.Sorcerer_スパークフレア.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_スパークフレア_D), Resources.Sorcerer_スパークフレア_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_スパークフレア_S), Resources.Sorcerer_スパークフレア_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ファイア), Resources.Sorcerer_ファイア.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ファイア_S), Resources.Sorcerer_ファイア_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ファイアランス), Resources.Sorcerer_ファイアランス.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ファイアランス_D), Resources.Sorcerer_ファイアランス_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ファイアランス_S), Resources.Sorcerer_ファイアランス_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_フリージングウェイブ), Resources.Sorcerer_フリージングウェイブ.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_フリージングウェイブ_D), Resources.Sorcerer_フリージングウェイブ_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_フリージングウェイブ_S), Resources.Sorcerer_フリージングウェイブ_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_フレイムサークル), Resources.Sorcerer_フレイムサークル.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_フレイムサークル_D), Resources.Sorcerer_フレイムサークル_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_フレイムサークル_S), Resources.Sorcerer_フレイムサークル_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ブリザードカレス), Resources.Sorcerer_ブリザードカレス.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ブリザードカレス_D), Resources.Sorcerer_ブリザードカレス_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ブリザードカレス_S), Resources.Sorcerer_ブリザードカレス_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ヘルファイア), Resources.Sorcerer_ヘルファイア.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ヘルファイア_D), Resources.Sorcerer_ヘルファイア_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ヘルファイア_S), Resources.Sorcerer_ヘルファイア_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_メテオインパクト), Resources.Sorcerer_メテオインパクト.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_メテオインパクト_D), Resources.Sorcerer_メテオインパクト_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_メテオインパクト_S), Resources.Sorcerer_メテオインパクト_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ライトニング), Resources.Sorcerer_ライトニング.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ライトニング_S), Resources.Sorcerer_ライトニング_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ライトニングスピア), Resources.Sorcerer_ライトニングスピア.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ライトニングスピア_D), Resources.Sorcerer_ライトニングスピア_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_ライトニングスピア_S), Resources.Sorcerer_ライトニングスピア_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_レーザーブラスト), Resources.Sorcerer_レーザーブラスト.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_レーザーブラスト_D), Resources.Sorcerer_レーザーブラスト_D.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_レーザーブラスト_S), Resources.Sorcerer_レーザーブラスト_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_詠唱), Resources.Sorcerer_詠唱.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_詠唱_S), Resources.Sorcerer_詠唱_S.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_通常攻撃), Resources.Sorcerer_通常攻撃.GenerateDifferenceHash() },
                { nameof(Resources.Sorcerer_通常攻撃_S), Resources.Sorcerer_通常攻撃_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_アサルトエッジ), Resources.Warrior_アサルトエッジ.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_アサルトエッジ_S), Resources.Warrior_アサルトエッジ_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_アタックレインフォース), Resources.Warrior_アタックレインフォース.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_アタックレインフォース_S), Resources.Warrior_アタックレインフォース_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_アークスタンプ), Resources.Warrior_アークスタンプ.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_アークスタンプ_S), Resources.Warrior_アークスタンプ_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_エクステンブレイド), Resources.Warrior_エクステンブレイド.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_エクステンブレイド_S), Resources.Warrior_エクステンブレイド_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_エンダーペイン), Resources.Warrior_エンダーペイン.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_エンダーペイン_S), Resources.Warrior_エンダーペイン_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ガードレインフォース), Resources.Warrior_ガードレインフォース.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ガードレインフォース_S), Resources.Warrior_ガードレインフォース_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_クラックバング), Resources.Warrior_クラックバング.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_クラックバング_S), Resources.Warrior_クラックバング_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_クランブルストーム), Resources.Warrior_クランブルストーム.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_クランブルストーム_S), Resources.Warrior_クランブルストーム_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_シールドバッシュ), Resources.Warrior_シールドバッシュ.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_シールドバッシュ_S), Resources.Warrior_シールドバッシュ_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ストライクスマッシュ), Resources.Warrior_ストライクスマッシュ.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ストライクスマッシュ_S), Resources.Warrior_ストライクスマッシュ_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_スマッシュ), Resources.Warrior_スマッシュ.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_スマッシュ_S), Resources.Warrior_スマッシュ_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_スラムアタック), Resources.Warrior_スラムアタック.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_スラムアタック_S), Resources.Warrior_スラムアタック_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ソニックブーム), Resources.Warrior_ソニックブーム.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ソニックブーム_S), Resources.Warrior_ソニックブーム_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ソリッドウォール), Resources.Warrior_ソリッドウォール.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ソリッドウォール_S), Resources.Warrior_ソリッドウォール_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ソードランページ), Resources.Warrior_ソードランページ.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ソードランページ_S), Resources.Warrior_ソードランページ_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ドラゴンテイル), Resources.Warrior_ドラゴンテイル.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ドラゴンテイル_S), Resources.Warrior_ドラゴンテイル_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_フォースインパクト), Resources.Warrior_フォースインパクト.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_フォースインパクト_S), Resources.Warrior_フォースインパクト_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ブレイズスラッシュ), Resources.Warrior_ブレイズスラッシュ.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ブレイズスラッシュ_S), Resources.Warrior_ブレイズスラッシュ_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ヘビースマッシュ), Resources.Warrior_ヘビースマッシュ.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ヘビースマッシュ_S), Resources.Warrior_ヘビースマッシュ_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ベヒモステイル), Resources.Warrior_ベヒモステイル.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_ベヒモステイル_S), Resources.Warrior_ベヒモステイル_S.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_通常攻撃), Resources.Warrior_通常攻撃.GenerateDifferenceHash() },
                { nameof(Resources.Warrior_通常攻撃_S), Resources.Warrior_通常攻撃_S.GenerateDifferenceHash() },
            };
        }

        protected override string Process(Bitmap bitmap)
        {
            // ハッシュ値が一致するスキル名を検索 (_S, _Dは選択・選択不可状態の画像のためスキル名からは削除)
            var hash = bitmap.GenerateDifferenceHash();
            // ハミング距離のしきい値(判定用途:似た画像が存在しない or スキルスロットにスキルを未設定)
            const int hamming_threshold = 10;

            var res = _skillDicionary
                .Select(x => new { x.Key, x.Value, Distance = hash.GetHammingDistance(x.Value) })
                .Where(x => x.Distance < hamming_threshold)
                .OrderBy(x => x.Distance)
                .Select(x => x.Key).FirstOrDefault();

            if (string.IsNullOrEmpty(res))
            {
                return null;
            }

            return res
                .Replace("Cestus_", "")
                .Replace("Fencer_", "")
                .Replace("Scout_", "")
                .Replace("Sorcerer_", "")
                .Replace("Warrior_", "")
                .Replace("_S", "")
                .Replace("_D", "");
        }

        public Work GetWork(string skillName)
        {
            var key = _skillDicionary.Keys.FirstOrDefault(x => x.IndexOf(skillName) != -1);

            if (string.IsNullOrEmpty(key))
            {
                return Work.不明;
            }

            if (key.IndexOf("Cestus_") != -1)
            {
                return Work.Cestus;
            }
            else if (key.IndexOf("Fencer_") != -1)
            {
                return Work.Fencer;
            }
            else if (key.IndexOf("Scout_") != -1)
            {
                return Work.Scout;
            }
            else if (key.IndexOf("Sorcerer_") != -1)
            {
                return Work.Sorcerer;
            }
            else if (key.IndexOf("Warrior_") != -1)
            {
                return Work.Warrior;
            }

            return Work.不明;
        }
    }
}

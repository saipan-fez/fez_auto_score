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
                { nameof(Resources.Cestus_アースバインド), Resources.Cestus_アースバインド.GenerateAverageHash() },
                { nameof(Resources.Cestus_アースバインド_S), Resources.Cestus_アースバインド_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_インテンスファイ), Resources.Cestus_インテンスファイ.GenerateAverageHash() },
                { nameof(Resources.Cestus_インテンスファイ_S), Resources.Cestus_インテンスファイ_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_エナジースフィア), Resources.Cestus_エナジースフィア.GenerateAverageHash() },
                { nameof(Resources.Cestus_エナジースフィア_S), Resources.Cestus_エナジースフィア_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_エンダーレイド), Resources.Cestus_エンダーレイド.GenerateAverageHash() },
                { nameof(Resources.Cestus_エンダーレイド_S), Resources.Cestus_エンダーレイド_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_ゲイザースマッシュ), Resources.Cestus_ゲイザースマッシュ.GenerateAverageHash() },
                { nameof(Resources.Cestus_ゲイザースマッシュ_S), Resources.Cestus_ゲイザースマッシュ_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_サイクロンディザスター), Resources.Cestus_サイクロンディザスター.GenerateAverageHash() },
                { nameof(Resources.Cestus_サイクロンディザスター_S), Resources.Cestus_サイクロンディザスター_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_シャットアウト), Resources.Cestus_シャットアウト.GenerateAverageHash() },
                { nameof(Resources.Cestus_シャットアウト_S), Resources.Cestus_シャットアウト_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_ショックウェイブ), Resources.Cestus_ショックウェイブ.GenerateAverageHash() },
                { nameof(Resources.Cestus_ショックウェイブ_S), Resources.Cestus_ショックウェイブ_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_ソリッドストライク), Resources.Cestus_ソリッドストライク.GenerateAverageHash() },
                { nameof(Resources.Cestus_ソリッドストライク_S), Resources.Cestus_ソリッドストライク_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_タワードミネーション), Resources.Cestus_タワードミネーション.GenerateAverageHash() },
                { nameof(Resources.Cestus_タワードミネーション_S), Resources.Cestus_タワードミネーション_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_ドレインクロー), Resources.Cestus_ドレインクロー.GenerateAverageHash() },
                { nameof(Resources.Cestus_ドレインクロー_S), Resources.Cestus_ドレインクロー_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_ノックインパクト), Resources.Cestus_ノックインパクト.GenerateAverageHash() },
                { nameof(Resources.Cestus_ノックインパクト_S), Resources.Cestus_ノックインパクト_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_ハードレインフォース), Resources.Cestus_ハードレインフォース.GenerateAverageHash() },
                { nameof(Resources.Cestus_ハードレインフォース_S), Resources.Cestus_ハードレインフォース_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_ハームアクティベイト), Resources.Cestus_ハームアクティベイト.GenerateAverageHash() },
                { nameof(Resources.Cestus_ハームアクティベイト_S), Resources.Cestus_ハームアクティベイト_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_ホーネットスティング), Resources.Cestus_ホーネットスティング.GenerateAverageHash() },
                { nameof(Resources.Cestus_ホーネットスティング_S), Resources.Cestus_ホーネットスティング_S.GenerateAverageHash() },
                { nameof(Resources.Cestus_通常攻撃), Resources.Cestus_通常攻撃.GenerateAverageHash() },
                { nameof(Resources.Cestus_通常攻撃_S), Resources.Cestus_通常攻撃_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_アクセラレーション), Resources.Fencer_アクセラレーション.GenerateAverageHash() },
                { nameof(Resources.Fencer_アクセラレーション_S), Resources.Fencer_アクセラレーション_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_イレイスマジック), Resources.Fencer_イレイスマジック.GenerateAverageHash() },
                { nameof(Resources.Fencer_イレイスマジック_S), Resources.Fencer_イレイスマジック_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_エリアルフォール), Resources.Fencer_エリアルフォール.GenerateAverageHash() },
                { nameof(Resources.Fencer_エリアルフォール_S), Resources.Fencer_エリアルフォール_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_オブティンプロテクト), Resources.Fencer_オブティンプロテクト.GenerateAverageHash() },
                { nameof(Resources.Fencer_オブティンプロテクト_S), Resources.Fencer_オブティンプロテクト_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_シャイニングクロス), Resources.Fencer_シャイニングクロス.GenerateAverageHash() },
                { nameof(Resources.Fencer_シャイニングクロス_S), Resources.Fencer_シャイニングクロス_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_ストライクダウン), Resources.Fencer_ストライクダウン.GenerateAverageHash() },
                { nameof(Resources.Fencer_ストライクダウン_S), Resources.Fencer_ストライクダウン_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_テンペストピアス), Resources.Fencer_テンペストピアス.GenerateAverageHash() },
                { nameof(Resources.Fencer_テンペストピアス_S), Resources.Fencer_テンペストピアス_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_デュアルストライク), Resources.Fencer_デュアルストライク.GenerateAverageHash() },
                { nameof(Resources.Fencer_デュアルストライク_S), Resources.Fencer_デュアルストライク_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_フィニッシュスラスト), Resources.Fencer_フィニッシュスラスト.GenerateAverageHash() },
                { nameof(Resources.Fencer_フィニッシュスラスト_S), Resources.Fencer_フィニッシュスラスト_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_フラッシュスティンガー), Resources.Fencer_フラッシュスティンガー.GenerateAverageHash() },
                { nameof(Resources.Fencer_フラッシュスティンガー_S), Resources.Fencer_フラッシュスティンガー_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_ペネトレイトスラスト), Resources.Fencer_ペネトレイトスラスト.GenerateAverageHash() },
                { nameof(Resources.Fencer_ペネトレイトスラスト_S), Resources.Fencer_ペネトレイトスラスト_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_ラピッドファンデヴ), Resources.Fencer_ラピッドファンデヴ.GenerateAverageHash() },
                { nameof(Resources.Fencer_ラピッドファンデヴ_S), Resources.Fencer_ラピッドファンデヴ_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_リバースキック), Resources.Fencer_リバースキック.GenerateAverageHash() },
                { nameof(Resources.Fencer_リバースキック_S), Resources.Fencer_リバースキック_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_ヴィガーエイド), Resources.Fencer_ヴィガーエイド.GenerateAverageHash() },
                { nameof(Resources.Fencer_ヴィガーエイド_S), Resources.Fencer_ヴィガーエイド_S.GenerateAverageHash() },
                { nameof(Resources.Fencer_通常攻撃), Resources.Fencer_通常攻撃.GenerateAverageHash() },
                { nameof(Resources.Fencer_通常攻撃_S), Resources.Fencer_通常攻撃_S.GenerateAverageHash() },
                { nameof(Resources.Scout_アローレイン), Resources.Scout_アローレイン.GenerateAverageHash() },
                { nameof(Resources.Scout_アローレイン_S), Resources.Scout_アローレイン_S.GenerateAverageHash() },
                { nameof(Resources.Scout_アームブレイク), Resources.Scout_アームブレイク.GenerateAverageHash() },
                { nameof(Resources.Scout_アームブレイク_S), Resources.Scout_アームブレイク_S.GenerateAverageHash() },
                { nameof(Resources.Scout_イーグルショット), Resources.Scout_イーグルショット.GenerateAverageHash() },
                { nameof(Resources.Scout_イーグルショット_S), Resources.Scout_イーグルショット_S.GenerateAverageHash() },
                { nameof(Resources.Scout_エアレイド), Resources.Scout_エアレイド.GenerateAverageHash() },
                { nameof(Resources.Scout_エアレイド_S), Resources.Scout_エアレイド_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ガードブレイク), Resources.Scout_ガードブレイク.GenerateAverageHash() },
                { nameof(Resources.Scout_ガードブレイク_S), Resources.Scout_ガードブレイク_S.GenerateAverageHash() },
                { nameof(Resources.Scout_クイックビート), Resources.Scout_クイックビート.GenerateAverageHash() },
                { nameof(Resources.Scout_クイックビート_S), Resources.Scout_クイックビート_S.GenerateAverageHash() },
                { nameof(Resources.Scout_クラッシュショット), Resources.Scout_クラッシュショット.GenerateAverageHash() },
                { nameof(Resources.Scout_クラッシュショット_S), Resources.Scout_クラッシュショット_S.GenerateAverageHash() },
                { nameof(Resources.Scout_コメットキャノン), Resources.Scout_コメットキャノン.GenerateAverageHash() },
                { nameof(Resources.Scout_コメットキャノン_S), Resources.Scout_コメットキャノン_S.GenerateAverageHash() },
                { nameof(Resources.Scout_スウィープキャノン), Resources.Scout_スウィープキャノン.GenerateAverageHash() },
                { nameof(Resources.Scout_スウィープキャノン_S), Resources.Scout_スウィープキャノン_S.GenerateAverageHash() },
                { nameof(Resources.Scout_スパイダーウェブ), Resources.Scout_スパイダーウェブ.GenerateAverageHash() },
                { nameof(Resources.Scout_スパイダーウェブ_S), Resources.Scout_スパイダーウェブ_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ダガーストライク), Resources.Scout_ダガーストライク.GenerateAverageHash() },
                { nameof(Resources.Scout_ダガーストライク_S), Resources.Scout_ダガーストライク_S.GenerateAverageHash() },
                { nameof(Resources.Scout_トゥルーショット), Resources.Scout_トゥルーショット.GenerateAverageHash() },
                { nameof(Resources.Scout_トゥルーショット_S), Resources.Scout_トゥルーショット_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ドッジシュート), Resources.Scout_ドッジシュート.GenerateAverageHash() },
                { nameof(Resources.Scout_ドッジシュート_S), Resources.Scout_ドッジシュート_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ハイド), Resources.Scout_ハイド.GenerateAverageHash() },
                { nameof(Resources.Scout_ハイド_S), Resources.Scout_ハイド_S.GenerateAverageHash() },
                { nameof(Resources.Scout_バーストキャノン), Resources.Scout_バーストキャノン.GenerateAverageHash() },
                { nameof(Resources.Scout_バーストキャノン_S), Resources.Scout_バーストキャノン_S.GenerateAverageHash() },
                { nameof(Resources.Scout_パニッシングストライク), Resources.Scout_パニッシングストライク.GenerateAverageHash() },
                { nameof(Resources.Scout_パニッシングストライク_D), Resources.Scout_パニッシングストライク_D.GenerateAverageHash() },
                { nameof(Resources.Scout_パニッシングストライク_S), Resources.Scout_パニッシングストライク_S.GenerateAverageHash() },
                { nameof(Resources.Scout_パワーシュート), Resources.Scout_パワーシュート.GenerateAverageHash() },
                { nameof(Resources.Scout_パワーシュート_S), Resources.Scout_パワーシュート_S.GenerateAverageHash() },
                { nameof(Resources.Scout_パワーブレイク), Resources.Scout_パワーブレイク.GenerateAverageHash() },
                { nameof(Resources.Scout_パワーブレイク_S), Resources.Scout_パワーブレイク_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ピアッシングシュート), Resources.Scout_ピアッシングシュート.GenerateAverageHash() },
                { nameof(Resources.Scout_ピアッシングシュート_S), Resources.Scout_ピアッシングシュート_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ファストショット), Resources.Scout_ファストショット.GenerateAverageHash() },
                { nameof(Resources.Scout_ファストショット_S), Resources.Scout_ファストショット_S.GenerateAverageHash() },
                { nameof(Resources.Scout_フリックショット), Resources.Scout_フリックショット.GenerateAverageHash() },
                { nameof(Resources.Scout_フリックショット_S), Resources.Scout_フリックショット_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ブレイズショット), Resources.Scout_ブレイズショット.GenerateAverageHash() },
                { nameof(Resources.Scout_ブレイズショット_S), Resources.Scout_ブレイズショット_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ホワイトバレット), Resources.Scout_ホワイトバレット.GenerateAverageHash() },
                { nameof(Resources.Scout_ホワイトバレット_S), Resources.Scout_ホワイトバレット_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ポイズンショット), Resources.Scout_ポイズンショット.GenerateAverageHash() },
                { nameof(Resources.Scout_ポイズンショット_S), Resources.Scout_ポイズンショット_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ポイズンブロウ), Resources.Scout_ポイズンブロウ.GenerateAverageHash() },
                { nameof(Resources.Scout_ポイズンブロウ_S), Resources.Scout_ポイズンブロウ_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ラッシュバレット), Resources.Scout_ラッシュバレット.GenerateAverageHash() },
                { nameof(Resources.Scout_ラッシュバレット_S), Resources.Scout_ラッシュバレット_S.GenerateAverageHash() },
                { nameof(Resources.Scout_レッグブレイク), Resources.Scout_レッグブレイク.GenerateAverageHash() },
                { nameof(Resources.Scout_レッグブレイク_S), Resources.Scout_レッグブレイク_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ヴァイパーバイト), Resources.Scout_ヴァイパーバイト.GenerateAverageHash() },
                { nameof(Resources.Scout_ヴァイパーバイト_S), Resources.Scout_ヴァイパーバイト_S.GenerateAverageHash() },
                { nameof(Resources.Scout_ヴォイドダークネス), Resources.Scout_ヴォイドダークネス.GenerateAverageHash() },
                { nameof(Resources.Scout_ヴォイドダークネス_S), Resources.Scout_ヴォイドダークネス_S.GenerateAverageHash() },
                { nameof(Resources.Scout_通常攻撃), Resources.Scout_通常攻撃.GenerateAverageHash() },
                { nameof(Resources.Scout_通常攻撃_S), Resources.Scout_通常攻撃_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_アイスジャベリン), Resources.Sorcerer_アイスジャベリン.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_アイスジャベリン_D), Resources.Sorcerer_アイスジャベリン_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_アイスジャベリン_S), Resources.Sorcerer_アイスジャベリン_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_アイスターゲット), Resources.Sorcerer_アイスターゲット.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_アイスターゲット_D), Resources.Sorcerer_アイスターゲット_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_アイスターゲット_S), Resources.Sorcerer_アイスターゲット_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_アイスボルト), Resources.Sorcerer_アイスボルト.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_アイスボルト_S), Resources.Sorcerer_アイスボルト_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_エレキドライブ), Resources.Sorcerer_エレキドライブ.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_エレキドライブ_D), Resources.Sorcerer_エレキドライブ_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_エレキドライブ_S), Resources.Sorcerer_エレキドライブ_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_グラビティキャプチャー), Resources.Sorcerer_グラビティキャプチャー.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_グラビティキャプチャー_D), Resources.Sorcerer_グラビティキャプチャー_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_グラビティキャプチャー_S), Resources.Sorcerer_グラビティキャプチャー_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_サンダーボルト), Resources.Sorcerer_サンダーボルト.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_サンダーボルト_D), Resources.Sorcerer_サンダーボルト_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_サンダーボルト_S), Resources.Sorcerer_サンダーボルト_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ジャッジメントレイ), Resources.Sorcerer_ジャッジメントレイ.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ジャッジメントレイ_D), Resources.Sorcerer_ジャッジメントレイ_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ジャッジメントレイ_S), Resources.Sorcerer_ジャッジメントレイ_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_スパークフレア), Resources.Sorcerer_スパークフレア.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_スパークフレア_D), Resources.Sorcerer_スパークフレア_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_スパークフレア_S), Resources.Sorcerer_スパークフレア_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ファイア), Resources.Sorcerer_ファイア.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ファイア_S), Resources.Sorcerer_ファイア_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ファイアランス), Resources.Sorcerer_ファイアランス.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ファイアランス_D), Resources.Sorcerer_ファイアランス_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ファイアランス_S), Resources.Sorcerer_ファイアランス_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_フリージングウェイブ), Resources.Sorcerer_フリージングウェイブ.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_フリージングウェイブ_D), Resources.Sorcerer_フリージングウェイブ_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_フリージングウェイブ_S), Resources.Sorcerer_フリージングウェイブ_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_フレイムサークル), Resources.Sorcerer_フレイムサークル.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_フレイムサークル_D), Resources.Sorcerer_フレイムサークル_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_フレイムサークル_S), Resources.Sorcerer_フレイムサークル_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ブリザードカレス), Resources.Sorcerer_ブリザードカレス.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ブリザードカレス_D), Resources.Sorcerer_ブリザードカレス_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ブリザードカレス_S), Resources.Sorcerer_ブリザードカレス_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ヘルファイア), Resources.Sorcerer_ヘルファイア.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ヘルファイア_D), Resources.Sorcerer_ヘルファイア_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ヘルファイア_S), Resources.Sorcerer_ヘルファイア_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_メテオインパクト), Resources.Sorcerer_メテオインパクト.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_メテオインパクト_D), Resources.Sorcerer_メテオインパクト_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_メテオインパクト_S), Resources.Sorcerer_メテオインパクト_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ライトニング), Resources.Sorcerer_ライトニング.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ライトニング_S), Resources.Sorcerer_ライトニング_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ライトニングスピア), Resources.Sorcerer_ライトニングスピア.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ライトニングスピア_D), Resources.Sorcerer_ライトニングスピア_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_ライトニングスピア_S), Resources.Sorcerer_ライトニングスピア_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_レーザーブラスト), Resources.Sorcerer_レーザーブラスト.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_レーザーブラスト_D), Resources.Sorcerer_レーザーブラスト_D.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_レーザーブラスト_S), Resources.Sorcerer_レーザーブラスト_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_詠唱), Resources.Sorcerer_詠唱.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_詠唱_S), Resources.Sorcerer_詠唱_S.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_通常攻撃), Resources.Sorcerer_通常攻撃.GenerateAverageHash() },
                { nameof(Resources.Sorcerer_通常攻撃_S), Resources.Sorcerer_通常攻撃_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_アサルトエッジ), Resources.Warrior_アサルトエッジ.GenerateAverageHash() },
                { nameof(Resources.Warrior_アサルトエッジ_S), Resources.Warrior_アサルトエッジ_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_アタックレインフォース), Resources.Warrior_アタックレインフォース.GenerateAverageHash() },
                { nameof(Resources.Warrior_アタックレインフォース_S), Resources.Warrior_アタックレインフォース_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_アークスタンプ), Resources.Warrior_アークスタンプ.GenerateAverageHash() },
                { nameof(Resources.Warrior_アークスタンプ_S), Resources.Warrior_アークスタンプ_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_エクステンブレイド), Resources.Warrior_エクステンブレイド.GenerateAverageHash() },
                { nameof(Resources.Warrior_エクステンブレイド_S), Resources.Warrior_エクステンブレイド_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_エンダーペイン), Resources.Warrior_エンダーペイン.GenerateAverageHash() },
                { nameof(Resources.Warrior_エンダーペイン_S), Resources.Warrior_エンダーペイン_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_ガードレインフォース), Resources.Warrior_ガードレインフォース.GenerateAverageHash() },
                { nameof(Resources.Warrior_ガードレインフォース_S), Resources.Warrior_ガードレインフォース_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_クラックバング), Resources.Warrior_クラックバング.GenerateAverageHash() },
                { nameof(Resources.Warrior_クラックバング_S), Resources.Warrior_クラックバング_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_クランブルストーム), Resources.Warrior_クランブルストーム.GenerateAverageHash() },
                { nameof(Resources.Warrior_クランブルストーム_S), Resources.Warrior_クランブルストーム_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_シールドバッシュ), Resources.Warrior_シールドバッシュ.GenerateAverageHash() },
                { nameof(Resources.Warrior_シールドバッシュ_S), Resources.Warrior_シールドバッシュ_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_ストライクスマッシュ), Resources.Warrior_ストライクスマッシュ.GenerateAverageHash() },
                { nameof(Resources.Warrior_ストライクスマッシュ_S), Resources.Warrior_ストライクスマッシュ_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_スマッシュ), Resources.Warrior_スマッシュ.GenerateAverageHash() },
                { nameof(Resources.Warrior_スマッシュ_S), Resources.Warrior_スマッシュ_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_スラムアタック), Resources.Warrior_スラムアタック.GenerateAverageHash() },
                { nameof(Resources.Warrior_スラムアタック_S), Resources.Warrior_スラムアタック_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_ソニックブーム), Resources.Warrior_ソニックブーム.GenerateAverageHash() },
                { nameof(Resources.Warrior_ソニックブーム_S), Resources.Warrior_ソニックブーム_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_ソリッドウォール), Resources.Warrior_ソリッドウォール.GenerateAverageHash() },
                { nameof(Resources.Warrior_ソリッドウォール_S), Resources.Warrior_ソリッドウォール_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_ソードランページ), Resources.Warrior_ソードランページ.GenerateAverageHash() },
                { nameof(Resources.Warrior_ソードランページ_S), Resources.Warrior_ソードランページ_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_ドラゴンテイル), Resources.Warrior_ドラゴンテイル.GenerateAverageHash() },
                { nameof(Resources.Warrior_ドラゴンテイル_S), Resources.Warrior_ドラゴンテイル_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_フォースインパクト), Resources.Warrior_フォースインパクト.GenerateAverageHash() },
                { nameof(Resources.Warrior_フォースインパクト_S), Resources.Warrior_フォースインパクト_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_ブレイズスラッシュ), Resources.Warrior_ブレイズスラッシュ.GenerateAverageHash() },
                { nameof(Resources.Warrior_ブレイズスラッシュ_S), Resources.Warrior_ブレイズスラッシュ_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_ヘビースマッシュ), Resources.Warrior_ヘビースマッシュ.GenerateAverageHash() },
                { nameof(Resources.Warrior_ヘビースマッシュ_S), Resources.Warrior_ヘビースマッシュ_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_ベヒモステイル), Resources.Warrior_ベヒモステイル.GenerateAverageHash() },
                { nameof(Resources.Warrior_ベヒモステイル_S), Resources.Warrior_ベヒモステイル_S.GenerateAverageHash() },
                { nameof(Resources.Warrior_通常攻撃), Resources.Warrior_通常攻撃.GenerateAverageHash() },
                { nameof(Resources.Warrior_通常攻撃_S), Resources.Warrior_通常攻撃_S.GenerateAverageHash() },
            };
        }

        public static int GetHammingDistance(BitArray lhs, BitArray rhs)
        {
            if (lhs.Length != rhs.Length)
            {
                return int.MinValue;
                // もしくは例外を発生。
                //throw new System.Exception("");
            }
            bool[] first = new bool[lhs.Length];
            lhs.CopyTo(first, 0);
            bool[] second = new bool[rhs.Length];
            rhs.CopyTo(second, 0);
            return first
                .Zip(second, (c1, c2) => new { c1, c2 })
                .Count(m => m.c1 != m.c2);
        }
        protected override string Process(Bitmap bitmap)
        {
            var hash = bitmap.GenerateAverageHash();
            // ハッシュ値が一致するスキル名を検索 (_S, _Dは選択・選択不可状態の画像のためスキル名からは削除)
            var res = _skillDicionary
                .OrderBy(x => GetHammingDistance(hash, x.Value)).FirstOrDefault().Key;

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

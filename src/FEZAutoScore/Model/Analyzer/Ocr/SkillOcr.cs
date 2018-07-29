using FEZAutoScore.Extension;
using FEZAutoScore.Model.Entity;
using FEZAutoScore.Properties;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FEZAutoScore.Model.Analyzer.Ocr
{
    public class SkillOcr : StringOcr
    {
        private Dictionary<string, byte[]> _skillDicionary;

        public SkillOcr()
        {
            _skillDicionary = new Dictionary<string, byte[]>()
            {
                { nameof(Resources.Cestus_アースバインド), Resources.Cestus_アースバインド.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_アースバインド_S), Resources.Cestus_アースバインド_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_インテンスファイ), Resources.Cestus_インテンスファイ.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_インテンスファイ_S), Resources.Cestus_インテンスファイ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_エナジースフィア), Resources.Cestus_エナジースフィア.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_エナジースフィア_S), Resources.Cestus_エナジースフィア_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_エンダーレイド), Resources.Cestus_エンダーレイド.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_エンダーレイド_S), Resources.Cestus_エンダーレイド_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ゲイザースマッシュ), Resources.Cestus_ゲイザースマッシュ.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ゲイザースマッシュ_S), Resources.Cestus_ゲイザースマッシュ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_サイクロンディザスター), Resources.Cestus_サイクロンディザスター.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_サイクロンディザスター_S), Resources.Cestus_サイクロンディザスター_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_シャットアウト), Resources.Cestus_シャットアウト.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_シャットアウト_S), Resources.Cestus_シャットアウト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ショックウェイブ), Resources.Cestus_ショックウェイブ.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ショックウェイブ_S), Resources.Cestus_ショックウェイブ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ソリッドストライク), Resources.Cestus_ソリッドストライク.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ソリッドストライク_S), Resources.Cestus_ソリッドストライク_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_タワードミネーション), Resources.Cestus_タワードミネーション.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_タワードミネーション_S), Resources.Cestus_タワードミネーション_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ドレインクロー), Resources.Cestus_ドレインクロー.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ドレインクロー_S), Resources.Cestus_ドレインクロー_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ノックインパクト), Resources.Cestus_ノックインパクト.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ノックインパクト_S), Resources.Cestus_ノックインパクト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ハードレインフォース), Resources.Cestus_ハードレインフォース.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ハードレインフォース_S), Resources.Cestus_ハードレインフォース_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ハームアクティベイト), Resources.Cestus_ハームアクティベイト.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ハームアクティベイト_S), Resources.Cestus_ハームアクティベイト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ホーネットスティング), Resources.Cestus_ホーネットスティング.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_ホーネットスティング_S), Resources.Cestus_ホーネットスティング_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_通常攻撃), Resources.Cestus_通常攻撃.GenerateHashFromBitmapData() },
                { nameof(Resources.Cestus_通常攻撃_S), Resources.Cestus_通常攻撃_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_アクセラレーション), Resources.Fencer_アクセラレーション.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_アクセラレーション_S), Resources.Fencer_アクセラレーション_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_イレイスマジック), Resources.Fencer_イレイスマジック.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_イレイスマジック_S), Resources.Fencer_イレイスマジック_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_エリアルフォール), Resources.Fencer_エリアルフォール.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_エリアルフォール_S), Resources.Fencer_エリアルフォール_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_オブティンプロテクト), Resources.Fencer_オブティンプロテクト.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_オブティンプロテクト_S), Resources.Fencer_オブティンプロテクト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_シャイニングクロス), Resources.Fencer_シャイニングクロス.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_シャイニングクロス_S), Resources.Fencer_シャイニングクロス_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_ストライクダウン), Resources.Fencer_ストライクダウン.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_ストライクダウン_S), Resources.Fencer_ストライクダウン_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_テンペストピアス), Resources.Fencer_テンペストピアス.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_テンペストピアス_S), Resources.Fencer_テンペストピアス_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_デュアルストライク), Resources.Fencer_デュアルストライク.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_デュアルストライク_S), Resources.Fencer_デュアルストライク_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_フィニッシュスラスト), Resources.Fencer_フィニッシュスラスト.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_フィニッシュスラスト_S), Resources.Fencer_フィニッシュスラスト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_フラッシュスティンガー), Resources.Fencer_フラッシュスティンガー.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_フラッシュスティンガー_S), Resources.Fencer_フラッシュスティンガー_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_ペネトレイトスラスト), Resources.Fencer_ペネトレイトスラスト.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_ペネトレイトスラスト_S), Resources.Fencer_ペネトレイトスラスト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_ラピッドファンデヴ), Resources.Fencer_ラピッドファンデヴ.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_ラピッドファンデヴ_S), Resources.Fencer_ラピッドファンデヴ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_リバースキック), Resources.Fencer_リバースキック.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_リバースキック_S), Resources.Fencer_リバースキック_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_ヴィガーエイド), Resources.Fencer_ヴィガーエイド.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_ヴィガーエイド_S), Resources.Fencer_ヴィガーエイド_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_通常攻撃), Resources.Fencer_通常攻撃.GenerateHashFromBitmapData() },
                { nameof(Resources.Fencer_通常攻撃_S), Resources.Fencer_通常攻撃_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_アローレイン), Resources.Scout_アローレイン.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_アローレイン_S), Resources.Scout_アローレイン_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_アームブレイク), Resources.Scout_アームブレイク.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_アームブレイク_S), Resources.Scout_アームブレイク_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_イーグルショット), Resources.Scout_イーグルショット.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_イーグルショット_S), Resources.Scout_イーグルショット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_エアレイド), Resources.Scout_エアレイド.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_エアレイド_S), Resources.Scout_エアレイド_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ガードブレイク), Resources.Scout_ガードブレイク.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ガードブレイク_S), Resources.Scout_ガードブレイク_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_クイックビート), Resources.Scout_クイックビート.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_クイックビート_S), Resources.Scout_クイックビート_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_クラッシュショット), Resources.Scout_クラッシュショット.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_クラッシュショット_S), Resources.Scout_クラッシュショット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_コメットキャノン), Resources.Scout_コメットキャノン.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_コメットキャノン_S), Resources.Scout_コメットキャノン_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_スウィープキャノン), Resources.Scout_スウィープキャノン.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_スウィープキャノン_S), Resources.Scout_スウィープキャノン_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_スパイダーウェブ), Resources.Scout_スパイダーウェブ.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_スパイダーウェブ_S), Resources.Scout_スパイダーウェブ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ダガーストライク), Resources.Scout_ダガーストライク.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ダガーストライク_S), Resources.Scout_ダガーストライク_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_トゥルーショット), Resources.Scout_トゥルーショット.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_トゥルーショット_S), Resources.Scout_トゥルーショット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ドッジシュート), Resources.Scout_ドッジシュート.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ドッジシュート_S), Resources.Scout_ドッジシュート_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ハイド), Resources.Scout_ハイド.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ハイド_S), Resources.Scout_ハイド_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_バーストキャノン), Resources.Scout_バーストキャノン.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_バーストキャノン_S), Resources.Scout_バーストキャノン_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_パニッシングストライク), Resources.Scout_パニッシングストライク.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_パニッシングストライク_D), Resources.Scout_パニッシングストライク_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_パニッシングストライク_S), Resources.Scout_パニッシングストライク_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_パワーシュート), Resources.Scout_パワーシュート.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_パワーシュート_S), Resources.Scout_パワーシュート_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_パワーブレイク), Resources.Scout_パワーブレイク.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_パワーブレイク_S), Resources.Scout_パワーブレイク_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ピアッシングシュート), Resources.Scout_ピアッシングシュート.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ピアッシングシュート_S), Resources.Scout_ピアッシングシュート_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ファストショット), Resources.Scout_ファストショット.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ファストショット_S), Resources.Scout_ファストショット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_フリックショット), Resources.Scout_フリックショット.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_フリックショット_S), Resources.Scout_フリックショット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ブレイズショット), Resources.Scout_ブレイズショット.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ブレイズショット_S), Resources.Scout_ブレイズショット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ホワイトバレット), Resources.Scout_ホワイトバレット.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ホワイトバレット_S), Resources.Scout_ホワイトバレット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ポイズンショット), Resources.Scout_ポイズンショット.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ポイズンショット_S), Resources.Scout_ポイズンショット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ポイズンブロウ), Resources.Scout_ポイズンブロウ.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ポイズンブロウ_S), Resources.Scout_ポイズンブロウ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ラッシュバレット), Resources.Scout_ラッシュバレット.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ラッシュバレット_S), Resources.Scout_ラッシュバレット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_レッグブレイク), Resources.Scout_レッグブレイク.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_レッグブレイク_S), Resources.Scout_レッグブレイク_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ヴァイパーバイト), Resources.Scout_ヴァイパーバイト.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ヴァイパーバイト_S), Resources.Scout_ヴァイパーバイト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ヴォイドダークネス), Resources.Scout_ヴォイドダークネス.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_ヴォイドダークネス_S), Resources.Scout_ヴォイドダークネス_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_通常攻撃短剣), Resources.Scout_通常攻撃短剣.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_通常攻撃短剣_S), Resources.Scout_通常攻撃短剣_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_通常攻撃弓), Resources.Scout_通常攻撃弓.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_通常攻撃弓_S), Resources.Scout_通常攻撃弓_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_通常攻撃銃), Resources.Scout_通常攻撃銃.GenerateHashFromBitmapData() },
                { nameof(Resources.Scout_通常攻撃銃_S), Resources.Scout_通常攻撃銃_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_アイスジャベリン), Resources.Sorcerer_アイスジャベリン.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_アイスジャベリン_D), Resources.Sorcerer_アイスジャベリン_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_アイスジャベリン_S), Resources.Sorcerer_アイスジャベリン_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_アイスターゲット), Resources.Sorcerer_アイスターゲット.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_アイスターゲット_D), Resources.Sorcerer_アイスターゲット_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_アイスターゲット_S), Resources.Sorcerer_アイスターゲット_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_アイスボルト), Resources.Sorcerer_アイスボルト.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_アイスボルト_S), Resources.Sorcerer_アイスボルト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_エレキドライブ), Resources.Sorcerer_エレキドライブ.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_エレキドライブ_D), Resources.Sorcerer_エレキドライブ_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_エレキドライブ_S), Resources.Sorcerer_エレキドライブ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_グラビティキャプチャー), Resources.Sorcerer_グラビティキャプチャー.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_グラビティキャプチャー_D), Resources.Sorcerer_グラビティキャプチャー_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_グラビティキャプチャー_S), Resources.Sorcerer_グラビティキャプチャー_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_サンダーボルト), Resources.Sorcerer_サンダーボルト.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_サンダーボルト_D), Resources.Sorcerer_サンダーボルト_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_サンダーボルト_S), Resources.Sorcerer_サンダーボルト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ジャッジメントレイ), Resources.Sorcerer_ジャッジメントレイ.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ジャッジメントレイ_D), Resources.Sorcerer_ジャッジメントレイ_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ジャッジメントレイ_S), Resources.Sorcerer_ジャッジメントレイ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_スパークフレア), Resources.Sorcerer_スパークフレア.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_スパークフレア_D), Resources.Sorcerer_スパークフレア_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_スパークフレア_S), Resources.Sorcerer_スパークフレア_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ファイア), Resources.Sorcerer_ファイア.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ファイア_S), Resources.Sorcerer_ファイア_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ファイアランス), Resources.Sorcerer_ファイアランス.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ファイアランス_D), Resources.Sorcerer_ファイアランス_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ファイアランス_S), Resources.Sorcerer_ファイアランス_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_フリージングウェイブ), Resources.Sorcerer_フリージングウェイブ.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_フリージングウェイブ_D), Resources.Sorcerer_フリージングウェイブ_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_フリージングウェイブ_S), Resources.Sorcerer_フリージングウェイブ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_フレイムサークル), Resources.Sorcerer_フレイムサークル.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_フレイムサークル_D), Resources.Sorcerer_フレイムサークル_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_フレイムサークル_S), Resources.Sorcerer_フレイムサークル_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ブリザードカレス), Resources.Sorcerer_ブリザードカレス.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ブリザードカレス_D), Resources.Sorcerer_ブリザードカレス_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ブリザードカレス_S), Resources.Sorcerer_ブリザードカレス_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ヘルファイア), Resources.Sorcerer_ヘルファイア.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ヘルファイア_D), Resources.Sorcerer_ヘルファイア_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ヘルファイア_S), Resources.Sorcerer_ヘルファイア_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_メテオインパクト), Resources.Sorcerer_メテオインパクト.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_メテオインパクト_D), Resources.Sorcerer_メテオインパクト_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_メテオインパクト_S), Resources.Sorcerer_メテオインパクト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ライトニング), Resources.Sorcerer_ライトニング.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ライトニング_S), Resources.Sorcerer_ライトニング_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ライトニングスピア), Resources.Sorcerer_ライトニングスピア.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ライトニングスピア_D), Resources.Sorcerer_ライトニングスピア_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_ライトニングスピア_S), Resources.Sorcerer_ライトニングスピア_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_レーザーブラスト), Resources.Sorcerer_レーザーブラスト.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_レーザーブラスト_D), Resources.Sorcerer_レーザーブラスト_D.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_レーザーブラスト_S), Resources.Sorcerer_レーザーブラスト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_詠唱), Resources.Sorcerer_詠唱.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_詠唱_S), Resources.Sorcerer_詠唱_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_通常攻撃), Resources.Sorcerer_通常攻撃.GenerateHashFromBitmapData() },
                { nameof(Resources.Sorcerer_通常攻撃_S), Resources.Sorcerer_通常攻撃_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_アサルトエッジ), Resources.Warrior_アサルトエッジ.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_アサルトエッジ_S), Resources.Warrior_アサルトエッジ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_アタックレインフォース), Resources.Warrior_アタックレインフォース.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_アタックレインフォース_S), Resources.Warrior_アタックレインフォース_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_アークスタンプ), Resources.Warrior_アークスタンプ.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_アークスタンプ_S), Resources.Warrior_アークスタンプ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_エクステンブレイド), Resources.Warrior_エクステンブレイド.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_エクステンブレイド_S), Resources.Warrior_エクステンブレイド_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_エンダーペイン), Resources.Warrior_エンダーペイン.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_エンダーペイン_S), Resources.Warrior_エンダーペイン_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ガードレインフォース), Resources.Warrior_ガードレインフォース.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ガードレインフォース_S), Resources.Warrior_ガードレインフォース_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_クラックバング), Resources.Warrior_クラックバング.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_クラックバング_S), Resources.Warrior_クラックバング_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_クランブルストーム), Resources.Warrior_クランブルストーム.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_クランブルストーム_S), Resources.Warrior_クランブルストーム_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_シールドバッシュ), Resources.Warrior_シールドバッシュ.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_シールドバッシュ_S), Resources.Warrior_シールドバッシュ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ストライクスマッシュ), Resources.Warrior_ストライクスマッシュ.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ストライクスマッシュ_S), Resources.Warrior_ストライクスマッシュ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_スマッシュ), Resources.Warrior_スマッシュ.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_スマッシュ_S), Resources.Warrior_スマッシュ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_スラムアタック), Resources.Warrior_スラムアタック.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_スラムアタック_S), Resources.Warrior_スラムアタック_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ソニックブーム), Resources.Warrior_ソニックブーム.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ソニックブーム_S), Resources.Warrior_ソニックブーム_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ソリッドウォール), Resources.Warrior_ソリッドウォール.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ソリッドウォール_S), Resources.Warrior_ソリッドウォール_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ソードランページ), Resources.Warrior_ソードランページ.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ソードランページ_S), Resources.Warrior_ソードランページ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ドラゴンテイル), Resources.Warrior_ドラゴンテイル.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ドラゴンテイル_S), Resources.Warrior_ドラゴンテイル_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_フォースインパクト), Resources.Warrior_フォースインパクト.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_フォースインパクト_S), Resources.Warrior_フォースインパクト_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ブレイズスラッシュ), Resources.Warrior_ブレイズスラッシュ.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ブレイズスラッシュ_S), Resources.Warrior_ブレイズスラッシュ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ヘビースマッシュ), Resources.Warrior_ヘビースマッシュ.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ヘビースマッシュ_S), Resources.Warrior_ヘビースマッシュ_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ベヒモステイル), Resources.Warrior_ベヒモステイル.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_ベヒモステイル_S), Resources.Warrior_ベヒモステイル_S.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_通常攻撃), Resources.Warrior_通常攻撃.GenerateHashFromBitmapData() },
                { nameof(Resources.Warrior_通常攻撃_S), Resources.Warrior_通常攻撃_S.GenerateHashFromBitmapData() },
            };
        }

        protected override string Process(Bitmap bitmap)
        {
            // MD5を取得
            var hash = bitmap.GenerateHashFromBitmapData();

            // ハッシュ値が一致するスキル名を検索 (_S, _Dは選択・選択不可状態の画像のためスキル名からは削除)
            var res = _skillDicionary
                .FirstOrDefault(x => hash.SequenceEqual(x.Value)).Key;

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

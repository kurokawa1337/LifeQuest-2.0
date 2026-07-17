using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LifeQuest.Models;
using LifeQuest.Services;

namespace LifeQuest.Views;

public partial class StatsView : UserControl
{
    private readonly MainWindow _main;
    private readonly I18n L = I18n.Instance;

    public StatsView(MainWindow main)
    {
        InitializeComponent();
        _main = main;
        L.LangChanged += Render;
    }

    private User Me => Session.Instance.CurrentUser!;

    public void Reload() => Render();

    private void Render()
    {
        TitleText.Text = L["stats"];
        var p = Me.Profile;
        int need = p.Level >= GameLogic.MaxLevel ? 0 : GameLogic.XpForLevel(p.Level);

        var cards = new (string label, string value, string color)[]
        {
            (L["statLevel"], p.Level.ToString(), "GoldBrush"),
            (L["statRank"], GameLogic.RankForLevel(p.Level), "GoldBrush"),
            (L["statTotalXp"], p.TotalXp.ToString(), "GreenBrush"),
            (L["statQuests"], p.QuestsDone.ToString(), "TextBrush"),
            (L["statProofs"], p.ProofsApproved.ToString(), "TextBrush"),
            (L["statPrestige"], "P" + p.Prestige, "WineBrush"),
            (L["statMult"], "×" + p.Multiplier.ToString("0.00"), "GoldBrush"),
            (L["statToNext"], p.Level >= GameLogic.MaxLevel ? L["max"] : (need - p.Xp) + " XP", "TextBrush"),
        };

        StatsGrid.Items.Clear();
        foreach (var c in cards)
            StatsGrid.Items.Add(BuildCard(c.label, c.value, c.color));

        bool atMax = p.Level >= GameLogic.MaxLevel;
        PrestigeBtn.Visibility = atMax ? Visibility.Visible : Visibility.Collapsed;
        PrestigeBtn.Content = L["prestigeBtn"];
    }

    private Border BuildCard(string label, string value, string colorKey)
    {
        var card = new Border { Style = (Style)FindResource("Card"), Margin = new Thickness(0, 0, 12, 12) };
        var sp = new StackPanel();
        sp.Children.Add(new TextBlock
        {
            Text = label, Style = (Style)FindResource("Muted"), TextWrapping = TextWrapping.Wrap
        });
        sp.Children.Add(new TextBlock
        {
            Text = value, FontSize = 26, FontWeight = FontWeights.Bold,
            Foreground = (Brush)FindResource(colorKey), Margin = new Thickness(0, 6, 0, 0)
        });
        card.Child = sp;
        return card;
    }

    private void Prestige_Click(object sender, RoutedEventArgs e)
    {
        if (!GameLogic.Prestige(Me.Profile)) return;
        App.Users.Update(Me);
        _main.RefreshHero();
        Render();
        _main.Toast($"{L["prestigeDone"]} ×{Me.Profile.Multiplier:0.00}", "xp");
        _main.ShowLevelUp(1, true);
    }
}

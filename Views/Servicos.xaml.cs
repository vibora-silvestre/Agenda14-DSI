namespace MauiApp1.Views;

public partial class Servicos : ContentPage
{
    public Servicos()
    {
        InitializeComponent();
    }

    private async void Voltar_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
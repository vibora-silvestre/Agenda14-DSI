using MauiApp1.Models;

namespace MauiApp1.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    App PropriedadesApp;

    private int qtdAdultos = 1;
    private int qtdCriancas = 0;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        PropriedadesApp = (App)Application.Current!;

        pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;

        dtpck_checkin.MinimumDate = DateTime.Today;
        dtpck_checkin.MaximumDate = DateTime.Today.AddMonths(1);

        DateTime dataCheckin =
            dtpck_checkin.Date.GetValueOrDefault(DateTime.Today);

        dtpck_checkout.MinimumDate = dataCheckin.AddDays(1);
        dtpck_checkout.MaximumDate = dataCheckin.AddMonths(6);
    }

    // Navegação para a tela independente de Serviços
    private async void IrParaServicos_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new Servicos());
    }

    // Navegação para a tela independente de Sobre
    private async void IrParaSobre_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new Sobre());
    }

    private void RemoverAdulto_Clicked(object? sender, EventArgs e)
    {
        if (qtdAdultos > 1)
        {
            qtdAdultos--;
            lbl_val_adultos.Text = qtdAdultos.ToString();
        }
    }

    private void AdicionarAdulto_Clicked(object? sender, EventArgs e)
    {
        if (qtdAdultos < 10)
        {
            qtdAdultos++;
            lbl_val_adultos.Text = qtdAdultos.ToString();
        }
    }

    private void RemoverCrianca_Clicked(object? sender, EventArgs e)
    {
        if (qtdCriancas > 0)
        {
            qtdCriancas--;
            lbl_val_criancas.Text = qtdCriancas.ToString();
        }
    }

    private void AdicionarCrianca_Clicked(object? sender, EventArgs e)
    {
        if (qtdCriancas < 10)
        {
            qtdCriancas++;
            lbl_val_criancas.Text = qtdCriancas.ToString();
        }
    }

    private async void Button_Clicked(object? sender, EventArgs e)
    {
        try
        {
            Hospedagem h = new Hospedagem
            {
                QuartoSelecionado = (Quarto)pck_quarto.SelectedItem,
                QntAdultos = qtdAdultos,
                QntCriancas = qtdCriancas,
                DataCheckIn = dtpck_checkin.Date ?? DateTime.Today,
                DataCheckOut = dtpck_checkout.Date ?? DateTime.Today.AddDays(1),
            };

            await Navigation.PushAsync(new HospedagemContratada()
            {
                BindingContext = h
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void dtpck_checkin_DateSelected(
        object? sender, DateChangedEventArgs e)
    {
        DateTime dataSelecionadaCheckin =
            e.NewDate.GetValueOrDefault(DateTime.Today);

        dtpck_checkout.MinimumDate =
            dataSelecionadaCheckin.AddDays(1);

        dtpck_checkout.MaximumDate =
            dataSelecionadaCheckin.AddMonths(6);
    }
}
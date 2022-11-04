using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppConsultaCEP.Model;
using AppConsultaCEP.Services;

namespace AppConsultaCEP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaCEP : ContentPage
    {
        public ConsultaCEP()
        {
            InitializeComponent();
        }

        private async void btn_Buscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                btn_Buscar.Text = "Carregando...";
                btn_Buscar.IsEnabled = false;

                CEP consulta = await DataService.getInfoFromCEP(txt_CEP.Text);

                lbl_CEP.Text += txt_CEP.Text;
                lbl_bairro.Text = consulta.descricao_bairro;
                lbl_cidade.Text = consulta.descricao_cidade;
                lbl_logradouro.Text = consulta.descricao;
                lbl_tipo_logradouro.Text = consulta.tipo;

                btn_Buscar.Text = "Nova Consulta";
                btn_Buscar.IsEnabled = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "Ok");
            }
        }
    }
}
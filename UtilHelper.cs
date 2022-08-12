using MPBA.Idea.Cadastros.PessoaFisica.Domain.Interfaces.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MPBA.Idea.Cadastros.PessoaFisica.Helpers
{
    public class UtilHelper : IUtilHelper
    {

        public UtilHelper() { }

        public bool ECpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public bool ETituloEleitor(String strTitulo)
        {
            int dig1; int dig2; int dig3; int dig4; int dig5; int dig6;
            int dig7; int dig8; int dig9; int dig10; int dig11;
            int dig12; int dv1; int dv2;

            if (strTitulo.Length == 0) //Validação do preenchimento
            {
                return false;
            }
            else
            {
                if (strTitulo.Length < 12)
                {
                    //Completar 12 dígitos             
                    strTitulo = "000000000000" + strTitulo;
                    strTitulo = strTitulo.Substring(strTitulo.Length - 12);
                }
                else if (strTitulo.Length > 12)
                {
                    return false;
                }
            }

            //Gravar posição dos caracteres
            dig1 = int.Parse(strTitulo[0].ToString());
            dig2 = int.Parse(strTitulo[1].ToString());
            dig3 = int.Parse(strTitulo[2].ToString());
            dig4 = int.Parse(strTitulo[3].ToString());
            dig5 = int.Parse(strTitulo[4].ToString());
            dig6 = int.Parse(strTitulo[5].ToString());
            dig7 = int.Parse(strTitulo[6].ToString());
            dig8 = int.Parse(strTitulo[7].ToString());
            dig9 = int.Parse(strTitulo[8].ToString());
            dig10 = int.Parse(strTitulo[9].ToString());
            dig11 = int.Parse(strTitulo[10].ToString());
            dig12 = int.Parse(strTitulo[11].ToString());

            //Cálculo para o primeiro dígito validador
            dv1 = (dig1 * 2) + (dig2 * 3) + (dig3 * 4) + (dig4 * 5) + (dig5 * 6) +
                    (dig6 * 7) + (dig7 * 8) + (dig8 * 9);
            dv1 = dv1 % 11;

            if (dv1 == 10)
            {
                dv1 = 0; //Se o resto for igual a 10, dv1 igual a zero
            }

            //Cálculo para o segundo dígito validador
            dv2 = (dig9 * 7) + (dig10 * 8) + (dv1 * 9);
            dv2 = dv2 % 11;

            if (dv2 == 10)
            {
                dv2 = 0; //Se o resto for igual a 10, dv1 igual a zero
            }

            //Validação dos dígitos validadores, após o cálculo realizado
            if (dig11 == dv1 && dig12 == dv2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ECertidaoNascimento(string nuCertidaoNascimento)
        {
            int[] multiplicador1 = new int[30] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] multiplicador2 = new int[31] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            string tmpCertidaoNascimento;
            string digito;
            int soma;
            int resto;

            // Remove os espaços a direita e a esquerda e a máscara (pontos e hífen)
            nuCertidaoNascimento = nuCertidaoNascimento.Trim().Replace(".", "").Replace("-", "");

            // Validação para garantir que a númeração possua os 32 dígitos
            if (nuCertidaoNascimento.Length != 32)
            {
                return false;
            }

            // Desconsidera o dígito verificador (-XX)
            tmpCertidaoNascimento = nuCertidaoNascimento.Substring(0, 30);
            soma = 0;

            // Lógica para identificação do primeiro dígito
            for (int i = 0; i < 30; i++)
            {
                soma += int.Parse(tmpCertidaoNascimento[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            digito = resto.ToString(); // Primeiro dígito

            // Lógica para identificação do segundo dígito
            tmpCertidaoNascimento += digito;
            soma = 0;
            for (int i = 0; i < 31; i++)
            {
                soma += int.Parse(tmpCertidaoNascimento[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;

            digito += resto.ToString(); // Concatenação com o segundo dígito

            return nuCertidaoNascimento.EndsWith(digito);
        }

        public bool ECep(string nuCep)
        {
            Regex Rgx = new Regex("^\\d{5}-\\d{3}$");
            if (!Rgx.IsMatch(nuCep)) {
                return false;
            } else
            {
                return true;
            }
        }

        public bool EEmailValido(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public bool ETelefoneValido(string nuTelefone)
        {
            Regex Rgx = new Regex("\\([1-9]\\d\\)\\s9?\\d{4}-\\d{4}");
            if (!Rgx.IsMatch(nuTelefone))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EPassaporteValido(string nuPassaport)
        {
            Regex Rgx = new Regex("[A-Za-z]{2}[0-9]{6}");

            return (Rgx.IsMatch(nuPassaport) && nuPassaport.Length == 8);
        }
    }
}

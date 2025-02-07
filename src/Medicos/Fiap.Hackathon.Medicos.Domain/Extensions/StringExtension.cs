using System.Text.RegularExpressions;

namespace Fiap.Hackathon.Medicos.Domain.Extensions
{
    public static class StringExtension
    {
        public static string RemoveMask(this string cpfFormatado)
        {
            if (string.IsNullOrWhiteSpace(cpfFormatado))
                return string.Empty;
            
            return Regex.Replace(cpfFormatado, @"\D", "");
        }

        public static bool EhCpfValido(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove a máscara do CPF
            cpf = Regex.Replace(cpf, @"\D", "");

            // CPF precisa ter 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais (ex: 111.111.111-11)
            if (new string(cpf[0], 11) == cpf)
                return false;

            // Calcula e verifica os dígitos verificadores
            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            int primeiroDigito = resto < 2 ? 0 : 11 - resto;

            tempCpf += primeiroDigito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            int segundoDigito = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(primeiroDigito.ToString() + segundoDigito.ToString());
        }

        public static bool EhCrmValido(this string crm)
        {
            if (string.IsNullOrWhiteSpace(crm))
                return false;

            // Regex para validar o formato do CRM: números + "/" + sigla do estado (UF)
            var regex = new Regex(@"^\d{4,6}/[A-Z]{2}$", RegexOptions.IgnoreCase);

            return regex.IsMatch(crm);
        }
    }
}

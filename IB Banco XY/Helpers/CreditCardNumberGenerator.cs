using Contratos.Helpers;
using Entidades;
using System;

namespace IB_Banco_XY.Helpers
{
    public class CreditCardNumberGenerator : INumberGenerator<TarjetaCredito>
    {
        private const int Cant_Code_Char = 15;

        private readonly Random _random = new();

        public string Generate_a_Code()
        {
            string acc_number = "";

            for (int i = 0; i <= Cant_Code_Char; i++)
            {
                acc_number += _random.Next(0, 9).ToString();

            }
            return acc_number;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Economy
{
    public class Wallet
    {
        private int balance;

        public Wallet()
        {
            balance = 0;
        }

        public Wallet(int balance)
        {
            this.balance = balance;
        }

        public int Balance { get => balance; set => balance = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALBANK.Classes;

public enum CardType
{
    None,
    Visa,
    MasterCard,
}
public class Card
{
    public CardType Type { get; private set; } = CardType.None;

    private double? balance;
    public double? Balance
    {

        get => balance;
        set
        {
            if (value == null)
            {
                balance = null;
            }
            if (value >= double.MaxValue)
            {
                throw new Exception("Value too small or too big");
            }
            balance = value;
        }
    }

    private string? cardnumber;
    public string CardNumber
    {
        get => cardnumber;
        set
        {
            if (value == null)
            {
                cardnumber = null;
            }
            if (value.Length != 16)
            {
                throw new Exception("Value card too small or too big");
            }
            if (value[0] == '4')
            {
                Type = CardType.Visa;
            }
            if (value[0] == '5')
            {
                Type = CardType.MasterCard;
            }
            cardnumber = value;            
        }
    }

    private int? cvv;
    public int? Cvv
    {
        get => cvv;
        set
        {
            if (value == null)
            {
                cvv = null;
            }
            cvv = value;            
        }
    }

    private DateTime? carddate;
    public DateTime? Carddate
    {
        get => carddate;
        set
        {
            if (value == null)
            {
                carddate = null;
            }
            if (value.Value.Year < DateTime.Now.Year || value.Value.Year > DateTime.Now.Year + 7)
            {
                throw new Exception("Value date too small or too big");
            }
            carddate = value;
        }
    }
}

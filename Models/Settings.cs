using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FINALBANK.Models;

public class Settings
{
    private bool isclose = false;
    public bool IsClose { get => isclose; set => isclose = value;}
}

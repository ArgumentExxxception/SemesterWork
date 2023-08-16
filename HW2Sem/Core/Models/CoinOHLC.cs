using System.Collections;

namespace HW2Sem.Models;

public record CoinOHLC(
    DateTime Time, 
    double Open,
    double High,
    double Low,
    double Close);
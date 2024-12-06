using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Net.Http;
using Shared;

namespace Client.Pages;
public class MediumMemBase : ComponentBase
{
    private int gridHeight = 4;
    int gridWidth = 6;
    int timer = 0;
}

public class MediumCard
{
    int flippedCards = 0;
    int pairs = 12;  
    enum cardFaces {M1=1, M2, M3, M4, M5, M6, M7, M8, 
                    M9, M10, M11, M12}
}
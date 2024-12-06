using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Net.Http;
using Shared;

namespace Client.Pages;
public class HardMemBase : ComponentBase
{
    private int gridHeight = 6;
    int gridWidth = 6;
    int timer = 0;
}

public class HardCard
{
    int flippedCards = 0;
    int pairs = 18;

    enum cardFaces {H1=1, H2, H3, H4, H5, H6, H7, H8,
                    H9, H10, H11, H12, H13, H14, H15, 
                    H16, H17, H18}
}
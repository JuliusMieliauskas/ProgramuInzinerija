using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Json;
using System.Net.Http;
using Shared;

namespace Client.Pages;
public class EasyMemBase : ComponentBase
{
    private int gridHeight = 4;
    int gridWidth = 4;
    int pairs = 8;
}

public class EasyCard
{
    bool isFlipped = false;
    enum cardFaces {E1=1, E2, E3, E4, E5, E6, E7, E8}
}
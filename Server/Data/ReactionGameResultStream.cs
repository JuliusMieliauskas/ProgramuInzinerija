using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using MyApp.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyApp.Data;
public class ReactionGameResultStream
{
    List<ReactionGameResult> reactionGameResults;   
    readonly string directoryInfo = Path.GetFullPath(".") + "\\Result.txt";
    public ReactionGameResultStream(){
        reactionGameResults = new List<ReactionGameResult>();
    }

    public async Task AddAsync(ReactionGameResult result){
        await Task.Run(() => reactionGameResults.Add(result));
        string json = JsonConvert.SerializeObject(reactionGameResults);
        File.WriteAllText(directoryInfo, json);
    }
    public async Task<List<ReactionGameResult>> GetAllAsync(){
        var text = File.ReadAllText(directoryInfo);
        var result = JsonConvert.DeserializeObject<List<ReactionGameResult>>(text);
        return result;
    }
    public async Task DeleteAsync(){
        await Task.Run(() => reactionGameResults.Clear());
    }



}
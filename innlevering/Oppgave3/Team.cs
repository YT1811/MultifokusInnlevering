
public class Team
{

    public string Name { get; set; } = "";
    public List<Player> players = new List<Player>();

    public Team() { }


    //add new player 

    public string AddPlayer(Player player)
    {
        var existedPlayer = players.FirstOrDefault(s => s.Id == player.Id);

        if (existedPlayer != null)
        {
            return $"Player with Id {player.Id} already exist";  //not added 
        }

        players.Add(player);
        return $"player with name {player.Name} added with Id: {player.Id}";

    }

    //remove 
    public string RemovePlayer(int playerId)
    {
        var existedPlayer = players.FirstOrDefault(s => s.Id == playerId);

        if (existedPlayer == null)
        {
            return $"Player with Id {playerId} not found.";
        }
        players.Remove(existedPlayer);
        return $"Player with Id {playerId} removed.";

    }

    // Find player by ID
    public string FindPlayerById(int playerId)
    {
        var player = players.FirstOrDefault(s => s.Id == playerId);

        if (player == null)
        {
            return $"Player with Id {playerId} not found.";
        }

        return $"Found player: {player.Name} with Id: {player.Id}.";
    }

    //Find players by rating
    public string FindPlayerByRating(int playerRating)
    {
        var player = players.FirstOrDefault(s => s.Rating == playerRating);

        if (player == null)
        {
            return $"Player with Rating {playerRating} not found.";
        }

        return $"Found player: {player.Name} with Rating: {player.Rating}.";
    }

    // Find players by name
    public string FindPlayersByName(string name)
    {
        var matchedPlayers = players.Where(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matchedPlayers.Count == 0)
        {
            return $"No players found with the name: {name}.";
        }

        return $"Found {matchedPlayers.Count} player(s) with the name {name}: " +
               string.Join(", ", matchedPlayers.Select(s => $"Id: {s.Id}, Name: {s.Name}"));
    }

    // Update player details
    public string UpdatePlayer(int playerId, string newName)
    {
        var player = players.FirstOrDefault(s => s.Id == playerId);

        if (player == null)
        {
            return $"Player with Id {playerId} not found.";
        }

        player.Name = newName;
        return $"Player with Id {playerId} updated. New Name: {newName}.";
    }




}
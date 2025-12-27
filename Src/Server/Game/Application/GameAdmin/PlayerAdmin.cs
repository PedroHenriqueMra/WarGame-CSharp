public class PlayerAdmin
{
    public bool TryCreatePlayer(CreatePlayerDTO data, out Player? player)
    {
        player = null;
        
        if (data.Name == null) return false;
        if (data.Name == "") return false;
        if (data.Name.Count() > 20) return false;
        if (data.Name.Count() < 3) return false;

        int id = 1; // editar depois
        player = new Player(id, data.Name);
        return true;
    }
}

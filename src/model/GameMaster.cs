namespace tcg
{
  class GameMaster
  {
    Client client1, client2;
    Host host;
    public GameMaster(Client client1, Client client2, Host host)
    {
      this.client1 = client1;
      this.client2 = client2;
      this.host = host;
    }
  }
}
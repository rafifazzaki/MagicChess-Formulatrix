using Moq;
namespace MagicChess.Test;

public class GameControllerTests
{
    Mock<IBattleArena> _arena;
    Mock<IBattleStore> _store;
    Mock<IRule> _rule;
    Mock<IPlayer> _player;
    Mock<IPlayerData> _playerData;
    Mock<IBattleLogger> _logBattle;
    
    GameController _game;
    

    // public GameController(IBattleArena arena, IBattleStore store, Rule rule, Dictionary<IPlayer, IPlayerData> playersData, IBattleLogger battleLogger, ILogger<GameController>? logger = null)
    [SetUp]
    public void Setup()
    {
        _arena = new();
        _store = new();
        _rule = new();
        _player = new();
        _playerData = new();
        _logBattle = new();
        Dictionary<IPlayer, IPlayerData> keyValuePairs = new();
        keyValuePairs.Add(_player.Object, _playerData.Object);
        _game = new GameController(_arena.Object, _store.Object, _rule.Object, keyValuePairs, _logBattle.Object);
    }

    [Test]
    // Positive Testing
    public void Test1()
    {
        // Assert.Pass();
        // Arrange


        // Action


        // Assert
    }
}
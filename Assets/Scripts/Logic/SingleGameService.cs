using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class SingleGameService
    {
        public static int SingleGame(int playerResult)
        {
            var gameResult = 9;

            var computerResult = GenerateComputerResult();

            if (playerResult == computerResult) gameResult = 0; // draw
            else
                switch (computerResult)
                {
                    case 0: // stone
                        if (playerResult == 1) gameResult = 1; // player lost
                        if (playerResult == 2) gameResult = 2; // player wins
                        break;
                    case 1: // scissors
                        if (playerResult == 2) gameResult = 1; // player lost
                        if (playerResult == 0) gameResult = 2; // player wins
                        break;
                    case 2: // paper
                        if (playerResult == 1) gameResult = 1; // player lost
                        if (playerResult == 0) gameResult = 2; // player wins
                        break;
                }

            Debug.Log($"Player's move: {ConvertMove(playerResult)}, computer's move: {ConvertMove(computerResult)}, game result: {ConvertResult(gameResult)}");
            return gameResult;
        }

        private static int GenerateComputerResult() => 
            Random.Range(0, 3);

        private static string ConvertMove(int move)
        {
            if (move == 0) return "КАМЕНЬ";
            if (move == 1) return "НОЖНИЦЫ";
            if (move == 2) return "БУМАГА";
            return "НЕВЕРНЫЙ ВВОД!!!";
        }

        private static string ConvertResult(int result)
        {
            if (result == 0) return "НИЧЬЯ!";
            if (result == 1) return "КОМПЬЮТЕР ВЫИГРАЛ!";
            if (result == 2) return "ИГРОК ВЫИГРАЛ!";
            if (result == 9) return "ОШИБКА!!!";
            return "НЕВЕРНЫЙ ВВОД!!!";
        }
    }
}
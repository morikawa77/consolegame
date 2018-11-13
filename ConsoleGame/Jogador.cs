using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    //A class representa um objeto do jogo
    // Para utilizar esse objeto em todo o jogo devo declar como public
    public class Jogador
    {
        //Atributos e Propriedades
        public char Personagem { get; set; }
        public ConsoleColor Cor { get; set; }
        public int PosicaoX { get; set; }
        public int PosicaoY { get; set; }


        //Metodos
        // Void so executa a operação (comando)
        //public void Movimentar(int x, int y, char personagem, ConsoleColor cor = ConsoleColor.Gray)
        //{
        //    Personagem = personagem;
        //    PosicaoX = x;
        //    PosicaoY = y;
        //    Cor = cor;
        //    Desenhar(PosicaoX, PosicaoY, Personagem, Cor);
        //}

        public void Desenhar(int x, int y, char personagem, ConsoleColor cor = ConsoleColor.Gray)
        {
            // Representação do jogador
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = cor;
            Console.Write(personagem);
        }
    }
}

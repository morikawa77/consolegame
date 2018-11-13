using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGame
{
    class Program
    {

        static void Main(string[] args)
        {
            ////Configuração do cenário Console
            //Console.BackgroundColor = ConsoleColor.Blue;
            //Console.Clear();         

            ////Criação do heroi
            ////Intanciando o objeto Jogador
            //Jogador heroi = new Jogador();
            //heroi.Id = 1;
            //heroi.Cor = ConsoleColor.White;
            //heroi.PosicaoX = 0;
            //heroi.PosicaoY = 0;
            //heroi.Movimentar(0,0);

            ////Criação do adversario
            ////Intanciando o objeto Jogador
            //Jogador adversario = new Jogador();
            //adversario.Id = 2;
            //adversario.Cor = ConsoleColor.Black;
            //adversario.PosicaoX = 10;
            //adversario.PosicaoY = 0;
            //adversario.Desenhar();

            //Console.ReadKey();

            //Valores 
            double velocidade = 100.0;
            double aceleracao = 0.5;
            int larguraJogo = 5;
            int vidas = 5;
            Console.BufferHeight = Console.WindowHeight = 20;
            Console.BufferWidth = Console.WindowWidth = 30;
            //Jogador principal
            Jogador jogadorHeroi = new Jogador();
            jogadorHeroi.Personagem = '@';
            jogadorHeroi.PosicaoX = 2;
            jogadorHeroi.PosicaoY = Console.WindowHeight - 1;
            jogadorHeroi.Cor = ConsoleColor.Yellow;
            //Level
            Random aleatorio = new Random();
            List<Jogador> jogadoresTabuleiro = new List<Jogador>();
            //Inicio do jogo
            while (true)
            {
                //Velecidade
                velocidade += aceleracao;
                if (velocidade > 400)
                {
                    velocidade = 400;
                }

                //Inicialização dos jogadores no tabuleiro
                bool perdeu = false;
                {
                    int chance = aleatorio.Next(0, 100);
                    if (chance < 10)
                    {
                        Jogador jogadorVida = new Jogador();
                        jogadorVida.Cor = ConsoleColor.Cyan;
                        jogadorVida.Personagem = '-';
                        jogadorVida.PosicaoX = aleatorio.Next(0, larguraJogo);
                        jogadorVida.PosicaoY = 0;
                        jogadoresTabuleiro.Add(jogadorVida);
                    }
                    else if (chance < 20)
                    {
                        Jogador jogadorAjuda = new Jogador();
                        jogadorAjuda.Cor = ConsoleColor.Cyan;
                        jogadorAjuda.Personagem = '*';
                        jogadorAjuda.PosicaoX = aleatorio.Next(0, larguraJogo);
                        jogadorAjuda.PosicaoY = 0;
                        jogadoresTabuleiro.Add(jogadorAjuda);
                    }
                    else
                    {
                        Jogador jogadorInimigo = new Jogador();
                        jogadorInimigo.Cor = ConsoleColor.Green;
                        jogadorInimigo.PosicaoX = aleatorio.Next(0, larguraJogo);
                        jogadorInimigo.PosicaoY = 0;
                        jogadorInimigo.Personagem = '#';
                        jogadoresTabuleiro.Add(jogadorInimigo);
                    }
                }
                //Controle do jogador pelo teclado
                while (Console.KeyAvailable)
                {
                    //Verifica teclas pressionadas
                    ConsoleKeyInfo teclaPressionada = Console.ReadKey(true);
                    if (teclaPressionada.Key == ConsoleKey.LeftArrow)
                    {
                        if (jogadorHeroi.PosicaoX - 1 >= 0)
                        {
                            jogadorHeroi.PosicaoX = jogadorHeroi.PosicaoX - 1;
                        }
                    }
                    else if (teclaPressionada.Key == ConsoleKey.RightArrow)
                    {
                        if (jogadorHeroi.PosicaoX + 1 < larguraJogo)
                        {
                            jogadorHeroi.PosicaoX = jogadorHeroi.PosicaoX + 1;
                        }
                    }
                }
                //Dinamica da movimentação dos jogadores no tabuleiro
                List<Jogador> jogadoresTabuleiroNovo = new List<Jogador>();
                for (int i = 0; i < jogadoresTabuleiro.Count; i++)
                {
                    Jogador jogadorVelho = jogadoresTabuleiro[i];
                    Jogador jogadorNovo = new Jogador();
                    jogadorNovo.PosicaoX = jogadorVelho.PosicaoX;
                    jogadorNovo.PosicaoY = jogadorVelho.PosicaoY + 1;
                    jogadorNovo.Personagem = jogadorVelho.Personagem;
                    jogadorNovo.Cor = jogadorVelho.Cor;

                    //Nivel do jogo
                    if (jogadorNovo.Personagem == '*' && jogadorNovo.PosicaoY == jogadorHeroi.PosicaoY && jogadorNovo.PosicaoX == jogadorHeroi.PosicaoX)
                    {
                        velocidade -= 20;
                    }
                    if (jogadorNovo.Personagem == '-' && jogadorNovo.PosicaoY == jogadorHeroi.PosicaoY && jogadorNovo.PosicaoX == jogadorHeroi.PosicaoX)
                    {
                        vidas++;
                    }
                    if (jogadorNovo.Personagem == '#' && jogadorNovo.PosicaoY == jogadorHeroi.PosicaoY && jogadorNovo.PosicaoX == jogadorHeroi.PosicaoX)
                    {
                        vidas--;
                        perdeu = true;
                        velocidade += 50;
                        if (velocidade > 400)
                        {
                            velocidade = 400;
                        }
                        if (vidas <= 0)
                        {
                            Desenhar(8, 10, "GAME OVER!!!", ConsoleColor.Red);
                            Desenhar(8, 12, "Press [enter] to exit", ConsoleColor.Red);
                            Console.ReadLine();
                            Environment.Exit(0);
                        }
                    }
                    if (jogadorNovo.PosicaoY < Console.WindowHeight)
                    {
                        jogadoresTabuleiroNovo.Add(jogadorNovo);
                    }
                }
                jogadoresTabuleiro = jogadoresTabuleiroNovo;
                Console.Clear();
                if (perdeu)
                {
                    jogadoresTabuleiro.Clear();
                    jogadorHeroi.Desenhar(jogadorHeroi.PosicaoX, jogadorHeroi.PosicaoY, 'X', ConsoleColor.Red);
                }
                else
                {
                    jogadorHeroi.Desenhar(jogadorHeroi.PosicaoX, jogadorHeroi.PosicaoY, jogadorHeroi.Personagem, jogadorHeroi.Cor);
                }

                //Movimentação do jogador principal no tabuleiro
                foreach (Jogador jogadorTabuleiro in jogadoresTabuleiro)
                {
                    jogadorHeroi.Desenhar(jogadorTabuleiro.PosicaoX, jogadorTabuleiro.PosicaoY, jogadorTabuleiro.Personagem, jogadorTabuleiro.Cor);
                }

                //Placar
                Desenhar(8, 4, "Vidas: " + vidas, ConsoleColor.White);
                Desenhar(8, 5, "Velocidade: " + velocidade, ConsoleColor.White);
                Desenhar(8, 6, "Aceleração: " + aceleracao, ConsoleColor.White);
                //Console.Beep();
                Thread.Sleep((int)(600 - velocidade));
            }

        }


        static void Desenhar(int x, int y, string texto, ConsoleColor cor = ConsoleColor.Gray)
        {
            // Representação do jogador
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = cor;
            Console.Write(texto);
        }
    }
}

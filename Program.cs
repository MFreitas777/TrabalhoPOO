using System;

class Program
{
    static void Main()
    {
        int opcao;

        do
        {
            Console.WriteLine("Menu Principal:");
            Console.WriteLine("1. Produtos");
            Console.WriteLine("2. Categorias");
            Console.WriteLine("3. Garantias");
            Console.WriteLine("4. Stocks");
            Console.WriteLine("5. Clientes");
            Console.WriteLine("6. Campanhas");
            Console.WriteLine("7. Vendas");
            Console.WriteLine("8. Marcas");
            Console.WriteLine("0. Sair");

            Console.Write("Escolha uma opção: ");
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Gerenciar Produtos");
                        break;
                    case 2:
                        Console.WriteLine("Gerenciar Categorias");
                        break;
                    case 3:
                        Console.WriteLine("Gerenciar Garantias");
                        break;
                    case 4:
                        Console.WriteLine("Gerenciar Stocks");
                        break;
                    case 5:
                        Console.WriteLine("Gerenciar Clientes");
                        break;
                    case 6:
                        Console.WriteLine("Gerenciar Campanhas");
                        break;
                    case 7:
                        Console.WriteLine("Gerenciar Vendas");
                        break;
                    case 8:
                        Console.WriteLine("Gerenciar Marcas");
                        break;
                    case 0:
                        Console.WriteLine("Saindo do sistema. Até logo!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }

            Console.WriteLine();
        } while (opcao != 0);
    }
}


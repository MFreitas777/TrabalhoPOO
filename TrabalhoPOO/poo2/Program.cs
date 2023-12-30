using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

class Program
{
    static List<Campanha> campanhas = new List<Campanha>();
    enum Categoria
    {
        Gaming,
        Eletrodomesticos,
        TVeSom,
        Informatica
    }
    class Campanha
    {
        public string Nome { get; set; }
        public Dictionary<string, decimal> ProdutosComDesconto { get; set; } = new Dictionary<string, decimal>();

        public void AdicionarProdutoComDesconto(string nomeProduto, decimal desconto)
        {
            if (!ProdutosComDesconto.ContainsKey(nomeProduto))
            {
                ProdutosComDesconto.Add(nomeProduto, desconto);
            }
            else
            {
                Console.WriteLine($"O produto '{nomeProduto}' já está na campanha com desconto de {ProdutosComDesconto[nomeProduto]}%.");
            }
        }
    }
    class Produto
    {
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public string Marca { get; set; }
        public decimal Preco { get; set; }
    }

    class Cliente
    {
        public string Nome { get; set; }
        public List<Produto> ProdutosComprados { get; set; } = new List<Produto>();
    }

    static List<Produto> produtos = new List<Produto>(); // Lista para armazenar informações de produtos
    static List<Cliente> clientes = new List<Cliente>(); // Lista para armazenar informações de clientes

    static void Main()
    {
        CarregarDadosDeArquivo();
        int opcao;

        do
        {
            Console.WriteLine("===================================");
            Console.WriteLine("           Menu Principal           ");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Produtos");
            Console.WriteLine("2. Stocks");
            Console.WriteLine("3. Clientes");
            Console.WriteLine("4. Campanhas");
            Console.WriteLine("5. Vendas");
            Console.WriteLine("6. Marcas");

            Console.WriteLine("0. Sair");

            Console.Write("Escolha uma opção: ");
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        GerenciarProdutos();
                        break;
                    case 2:
                        MostrarStocks();
                        break;
                    case 3:
                        GerenciarClientes();
                        break;
                    case 4:
                        GerenciarCampanhas();
                        break;
                    case 5:
                        GerenciarVendas();
                        break;
                    case 6:
                        MostrarMarcas();
                        break;
 

                    case 0:
                        Console.WriteLine("Saindo do sistema. Até logo!");
                        SalvarDadosEmArquivo();
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
            Console.WriteLine("===================================");

        } while (opcao != 0);
    }

    static void GerenciarProdutos()
    {
        int opcaoProdutos;

        do
        {
            Console.WriteLine("===================================");
            Console.WriteLine("        Menu de Produtos           ");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Adicionar Produto");
            Console.WriteLine("2. Remover Produto");
            Console.WriteLine("3. Ver Produtos");
            Console.WriteLine("0. Voltar ao Menu Principal");

            Console.Write("Escolha uma opção: ");
            if (int.TryParse(Console.ReadLine(), out opcaoProdutos))
            {
                switch (opcaoProdutos)
                {
                    case 1:
                        AdicionarProduto();
                        break;
                    case 2:
                        RemoverProduto();
                        break;
                    case 3:
                        MostrarProdutos();
                        break;
                    case 0:
                        Console.WriteLine("Voltando ao Menu Principal.");
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
            Console.WriteLine("===================================");

        } while (opcaoProdutos != 0);
    }

    static void AdicionarProduto()
    {
        Produto novoProduto = new Produto();

        Console.WriteLine("===================================");
        Console.Write("Digite o nome do produto: ");
        novoProduto.Nome = Console.ReadLine();

        Console.WriteLine("Escolha a categoria do produto:");
        foreach (Categoria categoria in Enum.GetValues(typeof(Categoria)))
        {
            Console.WriteLine($"{(int)categoria}. {categoria}");
        }

        if (Enum.TryParse(Console.ReadLine(), out Categoria categoriaEscolhida))
        {
            novoProduto.Categoria = categoriaEscolhida;
        }
        else
        {
            Console.WriteLine("Categoria inválida. O produto será adicionado sem categoria.");
        }

        Console.Write("Digite a quantidade em estoque do produto: ");
        if (int.TryParse(Console.ReadLine(), out int quantidadeEmEstoque))
        {
            novoProduto.QuantidadeEmEstoque = quantidadeEmEstoque;
        }
        else
        {
            Console.WriteLine("Quantidade em estoque inválida. O produto será adicionado com quantidade zero.");
        }

        Console.Write("Digite a marca do produto: ");
        novoProduto.Marca = Console.ReadLine();

        Console.Write("Digite o preço do produto: "); 
        if (decimal.TryParse(Console.ReadLine(), out decimal preco))
        {
            novoProduto.Preco = preco; 
        }
        else
        {
            Console.WriteLine("Preço inválido. O produto será adicionado sem preço.");
        }

        produtos.Add(novoProduto);
        Console.WriteLine($"Produto '{novoProduto.Nome}' adicionado com sucesso.");
    }

    static void RemoverProduto()
    {
        Console.WriteLine("===================================");
        Console.Write("Digite o nome do produto a ser removido: ");
        string produtoRemover = Console.ReadLine();

        Produto produto = produtos.Find(p => p.Nome.Equals(produtoRemover, StringComparison.OrdinalIgnoreCase));

        if (produto != null)
        {
            produtos.Remove(produto);
            Console.WriteLine($"Produto '{produtoRemover}' removido com sucesso.");
        }
        else
        {
            Console.WriteLine($"Produto '{produtoRemover}' não encontrado.");
        }
    }

    static void MostrarProdutos()
    {
        Console.WriteLine("===================================");
        Console.WriteLine("      Produtos adicionados:        ");
        foreach (var produto in produtos)
        {
            Console.WriteLine($"Nome: {produto.Nome}, Categoria: {produto.Categoria}, Quantidade em Estoque: {produto.QuantidadeEmEstoque}, Marca: {produto.Marca}, Preço:{produto.Preco}€");
        }
    }

    static void MostrarStocks()
    {
        Console.WriteLine("===================================");
        Console.WriteLine("         Stocks de Produtos         ");
        foreach (var produto in produtos)
        {
            Console.WriteLine($"Nome: {produto.Nome}, Categoria: {produto.Categoria}, Quantidade em Estoque: {produto.QuantidadeEmEstoque}, Marca: {produto.Marca}");
        }
    }

    static void GerenciarClientes()
    {
        int opcaoClientes;

        do
        {
            Console.WriteLine("===================================");
            Console.WriteLine("        Menu de Clientes           ");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Adicionar Cliente");
            Console.WriteLine("2. Remover Cliente");
            Console.WriteLine("3. Ver Clientes");
            Console.WriteLine("0. Voltar ao Menu Principal");

            Console.Write("Escolha uma opção: ");
            if (int.TryParse(Console.ReadLine(), out opcaoClientes))
            {
                switch (opcaoClientes)
                {
                    case 1:
                        AdicionarCliente();
                        break;
                    case 2:
                        RemoverCliente();
                        break;
                    case 3:
                        MostrarClientes();
                        break;
                    case 0:
                        Console.WriteLine("Voltando ao Menu Principal.");
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
            Console.WriteLine("===================================");

        } while (opcaoClientes != 0);
    }

    static void AdicionarCliente()
    {
        Cliente novoCliente = new Cliente();

        Console.WriteLine("===================================");
        Console.Write("Digite o nome do cliente: ");
        novoCliente.Nome = Console.ReadLine();

      
        int opcao;
        do
        {
            Console.WriteLine("Produtos disponíveis:");
            MostrarProdutos();

            Console.WriteLine("Selecione o número do produto que o cliente comprou (ou 0 para sair): ");
            if (int.TryParse(Console.ReadLine(), out opcao) && opcao >= 0 && opcao <= produtos.Count)
            {
                if (opcao != 0)
                {
                    Produto produtoSelecionado = produtos[opcao - 1];
                    novoCliente.ProdutosComprados.Add(produtoSelecionado);
                    Console.WriteLine($"Produto '{produtoSelecionado.Nome}' adicionado à lista de compras do cliente.");
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }

        } while (opcao != 0);

        clientes.Add(novoCliente);
        Console.WriteLine($"Cliente '{novoCliente.Nome}' adicionado com sucesso.");
    }

    static void RemoverCliente()
    {
        Console.WriteLine("===================================");
        Console.Write("Digite o nome do cliente a ser removido: ");
        string clienteRemover = Console.ReadLine();

        Cliente cliente = clientes.Find(c => c.Nome.Equals(clienteRemover, StringComparison.OrdinalIgnoreCase));

        if (cliente != null)
        {
            clientes.Remove(cliente);
            Console.WriteLine($"Cliente '{clienteRemover}' removido com sucesso.");
        }
        else
        {
            Console.WriteLine($"Cliente '{clienteRemover}' não encontrado.");
        }
    }

    static void MostrarClientes()
    {
        Console.WriteLine("===================================");
        Console.WriteLine("       Clientes cadastrados:        ");
        foreach (var cliente in clientes)
        {
            Console.WriteLine($"Nome: {cliente.Nome}");
            Console.WriteLine("Produtos Comprados:");
            foreach (var produtoComprado in cliente.ProdutosComprados)
            {
                Console.WriteLine($"- {produtoComprado.Nome}");
            }
            Console.WriteLine();
        }
    }

    static void MostrarMarcas()
    {
        Console.WriteLine("===================================");
        Console.WriteLine("        Marcas por Categoria        ");
        foreach (Categoria categoria in Enum.GetValues(typeof(Categoria)))
        {
            Console.WriteLine($"{categoria}: {string.Join(", ", produtos.Where(p => p.Categoria == categoria).Select(p => p.Marca).Distinct())}");
        }
    }
    static void GerenciarVendas()
    {
        int opcaoVendas;

        do
        {
            Console.WriteLine("===================================");
            Console.WriteLine("        Menu de Vendas              ");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Ver Artigos Vendidos"); 
            Console.WriteLine("0. Voltar ao Menu Principal");

            Console.Write("Escolha uma opção: ");
            if (int.TryParse(Console.ReadLine(), out opcaoVendas))
            {
                switch (opcaoVendas)
                {
                  
                    case 1:
                        VerArtigosVendidos(); 
                        break;
                    case 0:
                        Console.WriteLine("Voltando ao Menu Principal.");
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
            Console.WriteLine("===================================");

        } while (opcaoVendas != 0);
    }

    static void GerenciarCampanhas()
    {
        int opcaoCampanhas;

        do
        {
            Console.WriteLine("===================================");
            Console.WriteLine("       Menu de Campanhas            ");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Criar Campanha");
            Console.WriteLine("2. Ver Campanhas Atuais");
            Console.WriteLine("0. Voltar ao Menu Principal");

            Console.Write("Escolha uma opção: ");
            if (int.TryParse(Console.ReadLine(), out opcaoCampanhas))
            {
                switch (opcaoCampanhas)
                {
                    case 1:
                        CriarCampanha();
                        break;
                    case 2:
                        VerCampanhas();
                        break;
                    case 0:
                        Console.WriteLine("Voltando ao Menu Principal.");
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
            Console.WriteLine("===================================");

        } while (opcaoCampanhas != 0);
    }

    static void CriarCampanha()
    {
        Campanha novaCampanha = new Campanha();

        Console.WriteLine("===================================");
        Console.Write("Digite o nome da campanha: ");
        novaCampanha.Nome = Console.ReadLine();

        int opcao;
        do
        {
            Console.WriteLine("Produtos disponíveis:");
            MostrarProdutos();

            Console.WriteLine("Selecione o número do produto para adicionar à campanha (ou 0 para sair): ");
            if (int.TryParse(Console.ReadLine(), out opcao) && opcao >= 0 && opcao <= produtos.Count)
            {
                if (opcao != 0)
                {
                    Produto produtoSelecionado = produtos[opcao - 1];
                    Console.Write($"Digite o desconto para o produto '{produtoSelecionado.Nome}' (%): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal desconto))
                    {
                        novaCampanha.AdicionarProdutoComDesconto(produtoSelecionado.Nome, desconto);
                    }
                    else
                    {
                        Console.WriteLine("Desconto inválido. Tente novamente.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }

        } while (opcao != 0);

        campanhas.Add(novaCampanha);
        Console.WriteLine($"Campanha '{novaCampanha.Nome}' criada com sucesso.");
    }

    static void VerCampanhas()
    {
        Console.WriteLine("===================================");
        Console.WriteLine("     Campanhas Atuais              ");
        foreach (var campanha in campanhas)
        {
            Console.WriteLine($"Nome: {campanha.Nome}");
            Console.WriteLine("Produtos com Desconto:");
            foreach (var produtoDesconto in campanha.ProdutosComDesconto)
            {
                Console.WriteLine($"- Produto: {produtoDesconto.Key}, Desconto: {produtoDesconto.Value}%");
            }
            Console.WriteLine();
        }
    }
    static void CarregarDadosDeArquivo()
    {
        string path = "dados.txt"; 

        if (File.Exists(path))
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string linha;
                string secaoAtual = "";

                while ((linha = sr.ReadLine()) != null)
                {
                    if (linha.StartsWith("Produtos:"))
                    {
                        secaoAtual = "Produtos";
                    }
                    else if (linha.StartsWith("Clientes:"))
                    {
                        secaoAtual = "Clientes";
                    }
                    else if (linha.StartsWith("Campanhas:"))
                    {
                        secaoAtual = "Campanhas";
                    }
                    else if (!string.IsNullOrWhiteSpace(linha))
                    {
                        if (secaoAtual == "Produtos")
                        {
                            string[] dadosProduto = linha.Split(',');
                            Produto produto = new Produto
                            {
                                Nome = dadosProduto[0].Split(':')[1].Trim(),
                                Categoria = (Categoria)Enum.Parse(typeof(Categoria), dadosProduto[1].Split(':')[1].Trim()),
                                QuantidadeEmEstoque = int.Parse(dadosProduto[2].Split(':')[1].Trim()),
                                Marca = dadosProduto[3].Split(':')[1].Trim(),
                                Preco = decimal.Parse(dadosProduto[4].Split(':')[1].Trim())
                            };
                            produtos.Add(produto);
                        }
                        else if (secaoAtual == "Campanhas")
                        {
                            string nomeCampanha = linha.Split(':')[1].Trim();
                            Campanha campanha = new Campanha { Nome = nomeCampanha };

                            while ((linha = sr.ReadLine()) != null && !string.IsNullOrWhiteSpace(linha))
                            {
                                string[] dadosProdutoDesconto = linha.Split(',');
                                if (dadosProdutoDesconto.Length == 2)
                                {
                                    string nomeProduto = dadosProdutoDesconto[0].Trim();
                                    decimal desconto;
                                    if (decimal.TryParse(dadosProdutoDesconto[1].Split(':')[1].Trim().Trim('%'), out desconto))
                                    {
                                        campanha.AdicionarProdutoComDesconto(nomeProduto, desconto);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Erro ao ler o desconto para o produto '{nomeProduto}'.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Formato inválido para os dados da campanha.");
                                }
                            }

                            campanhas.Add(campanha);
                        }
                        else if (secaoAtual == "Clientes")
                        {
                            string nomeCliente = linha.Split(':')[1].Trim();
                            Cliente cliente = new Cliente { Nome = nomeCliente };


                            while ((linha = sr.ReadLine()) != null && !string.IsNullOrWhiteSpace(linha))
                            {
                                string nomeProduto = linha.Trim('-').Trim();
                                Produto produtoComprado = produtos.Find(p => p.Nome == nomeProduto);
                                if (produtoComprado != null)
                                {
                                    cliente.ProdutosComprados.Add(produtoComprado);
                                }
                            }

                            clientes.Add(cliente);
                        }
                       
                    }
                }
            }

            Console.WriteLine("Dados carregados do arquivo com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo de dados não encontrado. Iniciando com listas vazias.");
        }
    }
    static void SalvarDadosEmArquivo()
    {
     
        string path = "dados.txt"; 
        using (StreamWriter sw = File.CreateText(path))
        {
           
            sw.WriteLine("Produtos:");
            foreach (var produto in produtos)
            {
                sw.WriteLine($"Nome: {produto.Nome}, Categoria: {produto.Categoria}, Quantidade em Estoque: {produto.QuantidadeEmEstoque}, Marca: {produto.Marca}, Preço: {produto.Preco}");
            }

           
            sw.WriteLine("\nClientes:");
            foreach (var cliente in clientes)
            {
                sw.WriteLine($"Nome do Cliente: {cliente.Nome}");
                sw.WriteLine("Produtos Comprados:");
                foreach (var produtoComprado in cliente.ProdutosComprados)
                {
                    sw.WriteLine($"- {produtoComprado.Nome}");
                }
                sw.WriteLine(); 
            }
            sw.WriteLine("\nCampanhas:");
            foreach (var campanha in campanhas)
            {
            sw.WriteLine($"Nome da Campanha: {campanha.Nome}");
            sw.WriteLine("Produtos com Desconto:");
            foreach (var produtoDesconto in campanha.ProdutosComDesconto)
            {
                sw.WriteLine($"- Produto: {produtoDesconto.Key}, Desconto: {produtoDesconto.Value}%");
            }
            sw.WriteLine();
            }

          
        }

        Console.WriteLine("Dados salvos com sucesso no arquivo!");
    }
    static void VerArtigosVendidos()
    {
        Console.WriteLine("===================================");
        Console.WriteLine("      Artigos Vendidos              ");
        foreach (var cliente in clientes)
        {
            Console.WriteLine($"Cliente: {cliente.Nome}");
            Console.WriteLine("Artigos Comprados:");
            foreach (var produtoComprado in cliente.ProdutosComprados)
            {
                Console.WriteLine($"- {produtoComprado.Nome}");
            }
            Console.WriteLine();
        }
    }
}


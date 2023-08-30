namespace Execício_de_sistema_de_uma_biblioteca
{

    class Livros
    {
        public string Titulo { get; set; }
        public string Autor{ get; set; }
        public int AnoDePublicacao { get; set; }
        public int NumeroDePaginas { get; set; }
        public string Status { get; private set; }
        public Clientes ClienteEmprestado { get; set; }
        public  Livros(string titulo, string autor, int anoPublicacao, int numeroPaginas)
        {
            Titulo = titulo;
            Autor = autor;
            AnoDePublicacao = anoPublicacao;
            NumeroDePaginas = numeroPaginas;
            Status = "Disponível";
            ClienteEmprestado = null;          
        }

        public void Emprestar(Clientes cliente)
        {
            if (Status == "Disponível")
            {
                Status = "Emprestado";
                ClienteEmprestado = cliente;
            }
        }

        public void Devolver()
        {
            if (Status == "Emprestado")
            {
                Status = "Disponível";
                ClienteEmprestado = null;
            }
        }

        public void ExibirInformacoesLivros()
        {
            Console.WriteLine($"Título : {Titulo}");
            Console.WriteLine($"Autor : {Autor}");
            Console.WriteLine($"Ano de publicação : {AnoDePublicacao}");
            Console.WriteLine($"Número de páginas : {NumeroDePaginas}");
            Console.WriteLine($"Status : {Status}");
        }

    }

    class Clientes
    {
        public string Nome { get; set; }
        public string NumeroDeIdentificacao { get; set; }
        public List<Livros> LivroEmprestados { get; private set; }

        public  Clientes(string nome, string numeroDeIdentificacao)
        {
            Nome = nome;
            NumeroDeIdentificacao = numeroDeIdentificacao;
            LivroEmprestados = new List<Livros> ();
        }

        public void EmprestarLivro(Clientes cliente, Livros livro)
        {
            if (livro.Status == "Disponível")
            {
                livro.Emprestar(cliente);
                LivroEmprestados.Add(livro);
                livro.ClienteEmprestado = cliente;
            }
        }

        public void DevolverLivro(Livros livro)
        {
            if (LivroEmprestados.Contains(livro))
            {
                livro.Devolver();
                LivroEmprestados.Remove(livro);
            }
        }


        public void ExibirInformacoesCliente()
        {
            Console.WriteLine($"Nome do cliente: {Nome}");
            Console.WriteLine($"Número de identificação: {NumeroDeIdentificacao}");
            Console.WriteLine("Livros emprestados: ");

            if (LivroEmprestados.Count > 0)
            {
                foreach (var livro in LivroEmprestados)
                {
                    Console.WriteLine($"- {livro.Titulo}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum livro emprestado.");
            }
        }

    }

    class Biblioteca
    {
        public List<Livros> LivrosLista;
        public List<Clientes> ClienteLista;

        public Biblioteca()
        {
            LivrosLista = new List<Livros>();
            ClienteLista = new List<Clientes>();
        }

        public void AdicionarLivros(Livros Livro)
        {
            LivrosLista.Add(Livro);
        }

        public void RemoverLivros(Livros Livro)
        {
            LivrosLista.Remove(Livro);
        }

        public void AdicionarCliente(Clientes Cliente )
        {
            ClienteLista.Add(Cliente);
        }

        public void RemoverCliente(Clientes Cliente)
        {
            ClienteLista.Remove(Cliente);
        }

        public void ExibirLivrosDiponiveis()
        {
            foreach (var Livro in LivrosLista)
            {
                if(Livro.Status == "Disponível")
                {
                    Console.WriteLine($" {Livro.Titulo} ");
                }
            }
        }
        public void ExibirClientes()
        {
            Console.WriteLine("Clientes registrados : ");
            foreach (var Cliente in ClienteLista)
            {
                Console.WriteLine($"- {Cliente.Nome}");
            }
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Biblioteca biblioteca = new Biblioteca();

            while (true)
            {
                Console.WriteLine("----------- Biblioteca Fake ---------- \n Escolha a opção que deseja executar \n Opção 1 : Adicionar Livro.\n Opção 2 : Remover livro. \n Opção 3 : Emprestar livro. \n Opção 4 : Devolver Livro. \n Opção 5 : Registrar cliente. \n Opção 6 : Remover cliente. \n Opção 7 : Exibir informações sobre livro. \n Opção 8 : Exibir informações sobre Clientes.\n Opção 9 : Exibir lista de livros disponíveis.\n Digite 0 para finalizar. ");
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Programa encerrado.");
                        return;

                    case 1:
                        Console.WriteLine("Digite o titulo do livro");
                        string tituloDoLivro = Console.ReadLine();
                        Console.WriteLine("Digite o nome do autor");
                        string nomeAutor = Console.ReadLine();
                        Console.WriteLine("Digite o ano de publicação");
                        int anoPublicacao = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o número de páginas do livro");
                        int numeroPaginas = int.Parse(Console.ReadLine());
                        Livros novoLivro = new Livros(tituloDoLivro, nomeAutor, anoPublicacao, numeroPaginas);
                        biblioteca.AdicionarLivros(novoLivro);
                        break;

                    case 2:
                        Console.Write("Digite o título do livro que deseja remover: ");
                        string tituloLivroRemover = Console.ReadLine();

                        Livros livroRemover = biblioteca.LivrosLista.Find(livro => livro.Titulo == tituloLivroRemover);

                        if (livroRemover != null)
                        {
                            biblioteca.RemoverLivros(livroRemover);
                            Console.WriteLine($"Livro '{tituloLivroRemover}' removido com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine($"Livro '{tituloLivroRemover}' não encontrado na biblioteca.");
                        }
                        break;

                    case 3:
                        Console.Write("Digite o número de identificação do cliente: ");
                        string numeroIdentificacaoCliente = Console.ReadLine();

                        Clientes clienteAtual = biblioteca.ClienteLista.Find(cliente => cliente.NumeroDeIdentificacao == numeroIdentificacaoCliente);

                        if (clienteAtual == null)
                        {
                            Console.WriteLine($"Cliente com número de identificação '{numeroIdentificacaoCliente}' não encontrado.");
                            break;
                        }

                        Console.Write("Digite o título do livro que deseja emprestar: ");
                        string tituloLivroEmprestar = Console.ReadLine();

                        Livros livroEmprestar = biblioteca.LivrosLista.Find(livro => livro.Titulo == tituloLivroEmprestar);

                        if (livroEmprestar != null && livroEmprestar.Status == "Disponível")
                        {
                            clienteAtual.EmprestarLivro(clienteAtual, livroEmprestar); // Passando explicitamente o clienteAtual como argumento
                            Console.WriteLine($"Livro '{tituloLivroEmprestar}' emprestado para {clienteAtual.Nome}.");
                        }
                        else if (livroEmprestar != null && livroEmprestar.Status == "Emprestado")
                        {
                            Console.WriteLine($"Livro '{tituloLivroEmprestar}' indisponível.");
                        }
                        else
                        {
                            Console.WriteLine($"Livro '{tituloLivroEmprestar}' não encontrado na biblioteca.");
                        }
                        break;

                    case 4:
                        Console.Write("Digite o número de identificação do cliente: ");
                        string numeroIdentificacaoClienteDevolver = Console.ReadLine();

                        Clientes clienteDevolver = biblioteca.ClienteLista.Find(cliente => cliente.NumeroDeIdentificacao == numeroIdentificacaoClienteDevolver);

                        if (clienteDevolver == null)
                        {
                            Console.WriteLine($"Cliente com número de identificação '{numeroIdentificacaoClienteDevolver}' não encontrado.");
                            break;
                        }

                        Console.Write("Digite o título do livro que deseja devolver: ");
                        string tituloLivroDevolver = Console.ReadLine();

                        Livros livroDevolver = biblioteca.LivrosLista.Find(livro => livro.Titulo == tituloLivroDevolver);

                        if (livroDevolver != null)
                        {
                            clienteDevolver.DevolverLivro(livroDevolver); 
                            Console.WriteLine($"Livro '{tituloLivroDevolver}' devolvido com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine($"Livro '{tituloLivroDevolver}' não encontrado na biblioteca.");
                        }
                        break;

                    case 5:
                        Console.Write("Digite o nome do cliente: ");
                        string nomeCliente = Console.ReadLine();
                        Console.Write("Digite o número de identificação do cliente: ");
                        string numeroIdentificacao = Console.ReadLine();
                        Clientes novoCliente = new Clientes(nomeCliente, numeroIdentificacao);
                        biblioteca.AdicionarCliente(novoCliente);
                        Console.WriteLine($"Cliente '{nomeCliente}' registrado com sucesso.");
                        break;

                    case 6:
                        Console.Write("Digite o número de identificação do cliente que deseja remover: ");
                        string numeroIdentificacaoRemover = Console.ReadLine();
                        Clientes clienteRemover = biblioteca.ClienteLista.Find(cliente => cliente.NumeroDeIdentificacao == numeroIdentificacaoRemover);

                        if (clienteRemover != null)
                        {
                            biblioteca.RemoverCliente(clienteRemover);
                            Console.WriteLine($"Cliente '{clienteRemover.Nome}' removido com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine($"Cliente com número de identificação '{numeroIdentificacaoRemover}' não encontrado na biblioteca.");
                        }
                        break;

                    case 7:
                        Console.WriteLine("Digite o título do livro que deseja exibir informações:");
                        string tituloLivroExibir = Console.ReadLine();

                        Livros livroExibir = biblioteca.LivrosLista.Find(livro => livro.Titulo == tituloLivroExibir);

                        if (livroExibir != null)
                        {
                            Console.WriteLine("Informações do livro:");
                            livroExibir.ExibirInformacoesLivros();
                        }
                        else
                        {
                            Console.WriteLine($"Livro '{tituloLivroExibir}' não encontrado na biblioteca.");
                        }
                        break;

                    case 8:
                        Console.Write("Digite o número de identificação do cliente: ");
                        string numeroIdentificacaoClienteExibir = Console.ReadLine();

                        Clientes clienteExibir = biblioteca.ClienteLista.Find(cliente => cliente.NumeroDeIdentificacao == numeroIdentificacaoClienteExibir);

                        if (clienteExibir != null)
                        {
                            Console.WriteLine("Informações do cliente:");
                            clienteExibir.ExibirInformacoesCliente();
                        }
                        else
                        {
                            Console.WriteLine($"Cliente com número de identificação '{numeroIdentificacaoClienteExibir}' não encontrado na biblioteca.");
                        }
                        break;

                    case 9:
                        Console.WriteLine("Lista de livros disponíveis:");
                        biblioteca.ExibirLivrosDiponiveis();
                        break;


                }
            }     
                       
        }
    }
}
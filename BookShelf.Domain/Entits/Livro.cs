namespace BookShelf.Domain.Entits;

public class Livro
{
    public int LivroId { get; set; } // Chave Primária
    public string? Title { get; set; }
    public int Pages { get; set; }
    public string? Synopsis { get; set; }
    public int YearOfPublication { get; set; }

    public int AutorId { get; set; } // Chave Estrangeira
    public Autor? Autor { get; set; } // Relacionamento 1 para N com a classe Autor
}

namespace BookShelf.Domain.Entits;

public class Autor
{
    public int AutorId { get; set; } // Chave Primária
    public string? Name { get; set; }
    public string? Biography { get; set; }


    public ICollection<Livro>? Livros { get; set; }// Relacionamento N para 1 com a classe livro
   
}
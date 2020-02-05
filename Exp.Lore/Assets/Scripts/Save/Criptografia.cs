using System.IO;
using UnityEngine;

public class Criptografia
{
    public void criptografarArquivo(string nome, char chave)
    {
        StreamReader arquivoLeitura = new StreamReader(nome);
        StreamWriter arquivoCrip = new StreamWriter("crip" + nome);
        string linha = arquivoLeitura.ReadLine();
        int linhaN = 0;

        while (linha != null)
        {
            string linhaCriptografada = "";
            for (int i = 0; i < linha.Length; i++)
            {
                char letraOriginal;
                char letraCriptografada;
                if (i % 3 == 0)
                {
                    letraOriginal = linha[i];
                    letraCriptografada = (char)(letraOriginal + (chave + i - (chave - linhaN)));
                    linhaCriptografada += letraCriptografada;
                }
                else if (i % 2 == 0)
                {
                    letraOriginal = linha[i];
                    letraCriptografada = (char)(letraOriginal - (chave - (chave + i) + linhaN));
                    linhaCriptografada += letraCriptografada;
                }
                else
                {
                    letraOriginal = linha[i];
                    letraCriptografada = (char)(letraOriginal + (chave + chave - (i + linhaN)));
                    linhaCriptografada += letraCriptografada;
                }
            }
            arquivoCrip.WriteLine(linhaCriptografada);
            linha = arquivoLeitura.ReadLine();
            linhaN++;
        }
        arquivoLeitura.Close();
        arquivoCrip.Close();
    }

    public void descriptografarArquivo(string nome, char chave)
    {
        StreamReader arquivoCrip = new StreamReader("crip" + nome);
        string linhaCriptografada = arquivoCrip.ReadLine();
        int linhaN = 0;

        while (linhaCriptografada != null)
        {
            string linhaOriginal = "";
            for (int i = 0; i < linhaCriptografada.Length; i++)
            {
                char letraOriginal;
                char letraCriptografada;
                if (i % 3 == 0)
                {
                    letraCriptografada = linhaCriptografada[i];
                    letraOriginal = (char)(letraCriptografada - (Mathf.PI - (chave + i) - (chave - linhaN)));
                    linhaOriginal += letraOriginal;
                }
                else if (i % 2 == 0)
                {
                    letraCriptografada = linhaCriptografada[i];
                    letraOriginal = (char)(letraCriptografada + (chave - (chave + i) + linhaN));
                    linhaOriginal += letraOriginal;
                }
                else
                {
                    letraCriptografada = linhaCriptografada[i];
                    letraOriginal = (char)(letraCriptografada - (chave + chave - (i + linhaN)));
                    linhaOriginal += letraOriginal;
                }
            }
            linhaCriptografada = arquivoCrip.ReadLine();
            linhaN++;
        }

        arquivoCrip.Close();
    }
}

using Faturamento.Domain.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.ComponentModel;
using System.Reflection.Metadata;
using System.Text;

namespace Faturamento.Application.Serviços
{
    public class PdfService {
        public async Task<byte[]> GerarNotaFiscal(int notaFiscalId, List<ProdutosNotaFiscal> itens)
        {
            var pdf = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Content().Column(col =>
                    {
                        col.Item().Text($"Nota Fiscal #{notaFiscalId}")
                            .FontSize(20)
                            .Bold();

                        col.Item().Text(" ");

                        foreach (var item in itens)
                        {
                            col.Item().Text($"Produto: {item.produtoid} - Qtd: {item.quantidade}");
                        }
                    });
                });
            });

            return pdf.GeneratePdf();
        }
    }
}

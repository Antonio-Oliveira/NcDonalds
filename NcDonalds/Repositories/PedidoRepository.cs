﻿using Microsoft.EntityFrameworkCore;
using NcDonalds.Context;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoRecebido = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            var itensCarrinho = _carrinhoCompra.GetCarrinhoCompraItens();

            foreach (var item in itensCarrinho)
            {
                var pedidoDetalhe = new PedidoDetalhe()
                {
                    PedidoId = pedido.PedidoId,
                    Quantidade = item.Quantidade,
                    LancheId = item.Lanche.LancheId,
                    Preco = item.Lanche.Preco
                };

                _context.Add(pedidoDetalhe);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Pedido> GetPedidos()
        {
            return _context.Pedidos.ToList();
        }

        public IEnumerable<Pedido> GetPedidosPendentes()
        {
            var pedidos = _context.Pedidos.ToList();

            foreach(var pedido in pedidos)
            {
                var teste = pedido.UserId;
                var data = pedido.PedidoFinalizado;
            }
            
            return pedidos;
            
        }

        public Pedido GetPedidoById(int pedidoId)
        {
            var pedido = _context.Pedidos.Include(pd => pd.PedidoItens
                       .Where(pd => pd.PedidoId == pedidoId))
                       .FirstOrDefault(p => p.PedidoId == pedidoId);

            return pedido;
        }

        public async Task<bool> ConfirmarPedido(int pedidoId)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);

            if (pedido != null)
            {
                pedido.PedidoFinalizado = DateTime.Now;
                _context.Update(pedido);
                await _context.SaveChangesAsync();
                return true;
            }


            return false;
        }
    }
}
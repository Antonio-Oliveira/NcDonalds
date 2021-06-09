using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using NcDonalds.Models;
using NcDonalds.Repositories.Interfaces;
using NcDonalds.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NcDonalds.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IConfiguration _configuration;

        public AdminLancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository, IConfiguration configuration)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var lanches = _lancheRepository.Lanches;
            return View(lanches);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categorias = _categoriaRepository.Categorias;
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminLancheViewModel adminLancheVM)
        {

            Lanche lanche = new Lanche()
            {
                Nome = adminLancheVM.Nome,
                Preco = adminLancheVM.Preco,
                CategoriaId = adminLancheVM.CategoriaId,
                DescricaoCurta = adminLancheVM.DescricaoCurta,
                DescricaoDetalhada = adminLancheVM.DescricaoDetalhada,
                EmEstoque = adminLancheVM.EmEstoque,
            };


            lanche = (Lanche)await Save(adminLancheVM.Image, lanche);
            var result = await _lancheRepository.AddLanche(lanche);

            if (result)
            {
                return RedirectToAction("Index");
            }


            var categorias = _categoriaRepository.Categorias;
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Nome");
            return View(adminLancheVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var lanche = _lancheRepository.GetLancheById((int)id);

            if (lanche == null)
            {
                ModelState.AddModelError("", "Lanche não encontrado");
                return RedirectToAction("Index");
            }

            var adminLancheVM = new AdminLancheViewModel()
            {
                DescricaoCurta = lanche.DescricaoCurta,
                DescricaoDetalhada = lanche.DescricaoDetalhada,
                EmEstoque = lanche.EmEstoque,
                ImagemURL = lanche.ImagemURL,
                Nome = lanche.Nome,
                Preco = lanche.Preco,
                CategoriaId = lanche.CategoriaId,
                LancheId = lanche.LancheId
            };

            var categorias = _categoriaRepository.Categorias;
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Nome");
            return View(adminLancheVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminLancheViewModel adminLancheVM)
        {
            if (ModelState.IsValid)
            {

                var lanche = new Lanche()
                {
                    Nome = adminLancheVM.Nome,
                    Preco = adminLancheVM.Preco,
                    ImagemURL = adminLancheVM.ImagemURL,
                    LancheId = adminLancheVM.LancheId,
                    EmEstoque = adminLancheVM.EmEstoque,
                    DescricaoDetalhada = adminLancheVM.DescricaoDetalhada,
                    DescricaoCurta = adminLancheVM.DescricaoCurta,
                    CategoriaId = adminLancheVM.CategoriaId
                };

                lanche = (Lanche)await Save(adminLancheVM.Image, lanche);
                var result = await _lancheRepository.UpdateLanche(lanche);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Lanche não criado");

            }

            var categorias = _categoriaRepository.Categorias;
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Nome");
            return View(adminLancheVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                ModelState.AddModelError("", "Id do lanche vázio");
                return RedirectToAction("Index");
            }

            var result = await _lancheRepository.EmEstoque((int)id);

            if (!result)
            {
                ModelState.AddModelError("", "Erro ao tirar lanche do estoque");
                return RedirectToAction("Index", "AdminLanche");
            }

            return RedirectToAction("Index", "AdminLanche");
        }

        [HttpPost]
        public async Task<Lanche> Save(IFormFile file, Lanche lanche)
        {
            var imagemUrl = await Upload(file, lanche.Nome, lanche.Categoria);
            lanche.ImagemURL = imagemUrl;
            return lanche;
        }

        private async Task<string> Upload(IFormFile file, string nome, Categoria categoria)
        {
            //Obtem as configurações blob do 'appsettings.json' e atribui as variaveis
            var accountName = _configuration.GetSection("StorageConfiguration")["AccountName"];
            //var accountName = _configuration["StorageConfiguration: AccountName"];
            var accountKey = _configuration.GetSection("StorageConfiguration")["AccountKey"];
            //var accountKey = _configuration["StorageConfiguration: AccountKey"];
            var containerName = _configuration.GetSection("StorageConfiguration")["ContainerName"];
            //var containerName = _configuration["StorageConfiguration: ContainerName"];

            //Cria as credenciais de acesso do blob do Azure Storage e abre uma conexão com as APIs
            var storageCredentials = new StorageCredentials(accountName, accountKey);
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            var blobAzure = storageAccount.CreateCloudBlobClient();
            //Pega a referência do container que será feito o upload
            var container = blobAzure.GetContainerReference(containerName);

            //Atribui o nome do arquivo dentro do blob (podemos tratar com regex)
            string nameProductF = "group-category_" + categoria.ToString().ToLower() + "/" + "image_product" 
                + nome.Replace(" ", "_").ToLower() + ".jpg";
            var blob = container.GetBlockBlobReference(nameProductF);

            //Define o tipo do arquivo
            blob.Properties.ContentType = file.ContentType;
            //Realiza o upload do arquivo para o Blob
            await blob.UploadFromStreamAsync(file.OpenReadStream());

            //Obtem o URL do arquvivo no blob
            return blob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();

        }

    }
}


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

        public AdminLancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
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
            if (ModelState.IsValid)
            {
                Lanche lanche = new Lanche()
                {
                    
                };
                lanche = (Lanche) await Save(adminLancheVM.Image, adminLancheVM);
                var result = await _lancheRepository.AddLanche(lanche);

                if (result)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(lanche);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if(id == null)
            {
                return NotFound();
            }

            var lanche = _lancheRepository.GetLancheById((int)id);

            if(lanche == null)
            {
                ModelState.AddModelError("", "Lanche não encontrado");
                return RedirectToAction("Index");
            }

            var categorias = _categoriaRepository.Categorias;
            ViewData["CategoriaId"] = new SelectList(categorias, "CategoriaId", "Nome");
            return View(lanche);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("LancheId,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemURL,ImagemThumbURL,EmEstoque,CategoriaId")] Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                var result =  await _lancheRepository.UpdateLanche(lanche);

                if (result)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("","Lanche não criado");

            }

            return View(lanche);
        }

        public async Task<IActionResult> Delete(int? id)
        { 
            if(id == null)
            {
                ModelState.AddModelError("","Id do lanche vázio");
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

        public AdminLancheController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Save(IFormFile file, [FromForm] Lanche command)
        {
            var imagemUrl = await Upload(file);
            command.ImagemURL = imagemUrl;
            return Ok(command);

        }

        private async Task<string> Upload(IFormFile file)
        {
            //Obtem as configurações blob do 'appsettings.json' e atribui as variaveis
            var accountName = _configuration["StorageConfiguration: AccountName"];
            var accountKey = _configuration["StorageConfiguration: AccountKey"];
            var containerName = _configuration["StorageConfiguration: ContainerName"];

            //Cria as credenciais de acesso do blob do Azure Storage e abre uma conexão com as APIs
            var storageCredentials = new StorageCredentials(accountName, accountKey);
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            var blobAzure = storageAccount.CreateCloudBlobClient();
            //Pega a referência do container que será feito o upload
            var container = blobAzure.GetContainerReference(containerName);

            //Atribui o nome do arquivo dentro do blob (podemos tratar com regex)
            var blob = container.GetBlockBlobReference(file.FileName);

            //Define o tipo do arquivo
            blob.Properties.ContentType = file.ContentType;
            //Realiza o upload do arquivo para o Blob
            await blob.UploadFromStreamAsync(file.OpenReadStream());

            //Obtem o URL do arquvivo no blob
            return blob.SnapshotQualifiedStorageUri.PrimaryUri.ToString();

        }

    }
}


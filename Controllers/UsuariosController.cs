using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Sair()
        {
           HttpContext.Session.Clear();
           return RedirectToAction("Index", "Home");
        }

        public IActionResult administrador()
        {
            return View();
        }

        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());
        }

        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
            novoUser.Senha = Criptografo.TextoCriptografado(novoUser.Senha);

            new UsuarioService().incluirUsuario(novoUser);

            return RedirectToAction("ListaDeUsuarios");
        }

        public IActionResult EditarUsuario(int Id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar(Id));
        }

        [HttpPost]
        public IActionResult EditarUsuario(Usuario userEditado)
        {

            new UsuarioService().editarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");

        }
        public IActionResult ExcluirUsuario(int Id)
        {
            new UsuarioService().excluirUsuario(Id);
            return RedirectToAction("ListaDeUsuarios");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;

namespace BLL
{
    public class UsuarioBLL : Framework.BLL<UsuariosModel>, Framework.IBLL
    {
        public DataTable GetPermissionFromUser(Int64 user)
        {
            return new UsuariosDAL().GetPermissionFromUser(user);
        }
        public bool CheckUserPerfil(int codUsuario, int codPerfil)
        {
            return new UsuariosDAL().CheckUserPerfil(codUsuario, codPerfil);
        }
        public bool CheckUserPermission(int codUsuario, int codPermission)
        {
            return new UsuariosDAL().CheckUserPermission(codUsuario, codPermission);
        }
        public bool CheckUserPermission(ulong codUsuario, int codPermission)
        {
            return new UsuariosDAL().CheckUserPermission(Convert.ToInt32(codUsuario), codPermission);
        }
        public string GetLockup(Model.Framework.Model modelo, int type)
        {
            throw new NotImplementedException();
        }
        public bool InsertPerfil(int codPerfil, int codUsuario)
        {
            return new UsuariosDAL().InsertPerfil(codPerfil, codUsuario);
        }
        public bool RemovePerfil(int codPerfil, int codUsuario)
        {
            return new UsuariosDAL().RemovePerfil(codPerfil, codUsuario);
        }
        public List<UsuariosModel> GetUsers()
        {
            UsuariosDAL dal = new UsuariosDAL();
            return dal.GetUsers();
        }
        public List<LockUpModel> LoockUp(string parteNome)
        {
            UsuariosDAL dal = new UsuariosDAL();
            return dal.LoockUp(parteNome);
        }
        public List<LockUpModel> LoockUpVend(string parteNome)
        {
            UsuariosDAL dal = new UsuariosDAL();
            return dal.LoockUpVend(parteNome);
        }
        public string GetName(Int32 codUser)
        {
            UsuariosDAL dal = new UsuariosDAL();
            return dal.GetName(codUser);
        }
        public bool TrocaSenhaQuick(int codUser, string nova)
        {
            UsuariosDAL dal = new UsuariosDAL();
            return dal.TrocaSenhaQuick(codUser, nova);
        }
        public bool TrocaSenha(int codUser, string antiga, string nova)
        {
            UsuariosDAL dal = new UsuariosDAL();
            return dal.TrocaSenha(codUser, antiga, nova);
        }
        public UsuariosModel GetUser(string login, string senha)
        {
            UsuariosDAL dal = new UsuariosDAL();
            UsuariosModel usu = dal.GetUser(login,senha);

            if (usu.codigo == 0)
                throw new MissingMemberException("Usuario e/ou senha incorreto(os)");

            return usu;
        }

        //public bool FrameworkInsert(FrameworkModel.Framework.Model modelo)
        //{
        //    return new UsuariosDAL().FrameworkInsert(modelo);
        //}

        //public object FrameworkGet(FrameworkModel.Framework.Model modelo)
        //{
        //    UsuariosDAL dal = new UsuariosDAL();
        //    dal.CustomOrder = "nome";
        //    return dal.FrameworkGet(modelo);
        //}

        //public object FrameworkGetOne(FrameworkModel.Framework.Model modelo, int recno)
        //{
        //    UsuariosDAL dal = new UsuariosDAL();
        //    dal.CustomWhere = "codigo =" + recno;
        //    return dal.FrameworkGet(modelo).First();
        //}

        //public bool FrameworkUpdate(FrameworkModel.Framework.Model modelo)
        //{
        //    return new UsuariosDAL().FrameworkUpdate(modelo);
        //}

        //public bool FrameworkDelete(FrameworkModel.Framework.Model modelo)
        //{
        //    return new UsuariosDAL().FrameworkDelete(modelo);
        //}

        //public object FrameworkGetCustom(FrameworkModel.Framework.Model modelo, string _customParam)
        //{
        //    UsuariosDAL dal = new UsuariosDAL();
        //    dal.CustomWhere = _customParam;
        //    return dal.FrameworkGet(modelo);
        //}
    }
}

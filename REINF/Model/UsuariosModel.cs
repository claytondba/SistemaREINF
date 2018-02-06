using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Framework;

namespace Model
{
    public class UsuariosModel: Framework.Model
    {
        public UsuariosModel()
        {
            Entity = "usuarios";
        }
        [PrimaryKey(isKey=true)]
        public UInt64 codigo { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public string senha1 { get; set; }
        public string senha2 { get; set; }
        public int ativada { get; set; }
        public DateTime validade { get; set; }
        public string classe { get; set; }
        public int inclusao { get; set; }
        public int alteracao { get; set; }
        public int impressao { get; set; }
        public int exclusao { get; set; }
        public int recupera { get; set; }
        public int consulta { get; set; }
        public DateTime troca_senha { get; set; }
        public string email { get; set; }
        public int ver_custo { get; set; }
        public int ver_preco { get; set; }
        public Int64 vend { get; set; }
        public string formularios { get; set; }
        public string usu { get; set; }
        public string tipo { get; set; }
        public string depto { get; set; }
        [NotField(isNotField=true)]
        public DateTime data_base { get; set; }
        public string cracha { get; set; }
        public string login { get; set; }
        public string op { get; set; }
        public UInt32 super { get; set; }
        public UInt32 setup { get; set; }
        public UInt32 insp { get; set; }
        public bool ver_especial { get; set; }
        public Int64 form_linhas { get; set; }
        public bool not_aprov { get; set; }
        public string ver_compra_user { get; set; }
    }
}

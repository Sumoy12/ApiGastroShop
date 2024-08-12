namespace ProyectoDAW.Models
{
    public class Inventario
    {
        public int InventarioId { get; set; }
        public Producto Producto { get; set; } = null!;
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Tipo { get; set; } = null!;
        public bool Estado { get; set; }
        public Proveedor Proveedor { get; set; } = null!;
        public int ProveedorId { get; set; }
    }
}

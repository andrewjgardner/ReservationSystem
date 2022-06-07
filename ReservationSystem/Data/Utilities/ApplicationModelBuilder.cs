using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Data.Utilities
{
    public class ApplicationModelBuilder
    {
        ModelBuilder _modelBuilder;

        public ApplicationModelBuilder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            ModelArea();
            ModelPerson();
            ModelRestaurant();
            ModelSitting();
            ModelTable();
            //ModelReservation();
            //ModelReservationTable();
        }

        private void ModelArea()
        {
            _modelBuilder.Entity<Area>().HasOne(a => a.Restaurant).WithMany(r => r.Areas).OnDelete(DeleteBehavior.Restrict);
        }

        private void ModelPerson()
        {
            _modelBuilder.Entity<Person>().HasOne(p => p.Restaurant).WithMany(r => r.People).OnDelete(DeleteBehavior.Restrict);
        }

        private void ModelRestaurant()
        {
            _modelBuilder.Entity<Reservation>().HasOne(r => r.Sitting).WithMany(s => s.Reservations).OnDelete(DeleteBehavior.Restrict);
            _modelBuilder.Entity<Reservation>().HasOne(r => r.ReservationOrigin).WithMany(ro => ro.Reservations).OnDelete(DeleteBehavior.Restrict);
            _modelBuilder.Entity<Reservation>().HasOne(r => r.ReservationStatus).WithMany(rs => rs.Reservations).OnDelete(DeleteBehavior.Restrict);
            _modelBuilder.Entity<Reservation>().HasOne(r => r.Customer).WithMany(c => c.Reservations).OnDelete(DeleteBehavior.Restrict);
        }

        private void ModelSitting()
        {
            _modelBuilder.Entity<Sitting>().HasOne(s => s.Restaurant).WithMany(r => r.Sittings).OnDelete(DeleteBehavior.Restrict);
            _modelBuilder.Entity<Sitting>().HasOne(s => s.SittingType).WithMany(st => st.Sittings).OnDelete(DeleteBehavior.Restrict);
        }

        private void ModelTable()
        {
            _modelBuilder.Entity<Table>().HasOne(a => a.Area).WithMany(a => a.Tables).OnDelete(DeleteBehavior.Restrict);
        }

        private void ModelReservation()
        {
            //Model building for database only entity 

            //_modelBuilder.Entity<Reservation>()
            //    .HasMany(r => r.Tables).WithMany(t => t.Reservations)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "ReservationTables",
            //        rt => rt.HasOne<Table>().WithMany().OnDelete(DeleteBehavior.Restrict),
            //        rt => rt.HasOne<Reservation>().WithMany().OnDelete(DeleteBehavior.Restrict));
        }


        //private void ModelReservationTable()
        //{
        //    _modelBuilder.Entity<ReservationTable>()
        //        .HasKey(t => new { t.ReservationId, t.TableId });
        //}

        private void ModelUserTable()
        {
        }
    }
}

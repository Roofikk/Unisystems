using Microsoft.EntityFrameworkCore;
using Unisystems.ClassroomAccount.DataContext;
using Unisystems.ClassroomAccount.DataContext.Entities;
using Unisystems.ClassroomAccount.WebApi.Models.TechEquipments;

namespace Unisystems.ClassroomAccount.WebApi.TechEquipmentService;

public class TechEquipmentService
{
    private readonly ILogger<TechEquipmentService> _logger;
    private readonly ClassroomContext _context;

    public TechEquipmentService(ILogger<TechEquipmentService> logger, ClassroomContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IQueryable<TechEquipment> GetAll()
    {
        return _context.TechEquipments
            .Include(t => t.Classroom)
            .AsNoTracking();
    }

    public async Task<IEnumerable<TechEquipment>> GetAllAsync() => await GetAll().ToListAsync();

    public async Task<TechEquipment?> GetByIdAsync(int id)
    {
        return await _context.TechEquipments
            .Include(t => t.Classroom)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.EquipmentId == id);
    }

    public IQueryable<TechEquipment> GetByClassroomId(int classroomId)
    {
        return _context.TechEquipments
            .Include(t => t.Classroom)
            .AsNoTracking()
            .Where(t => t.ClassroomId == classroomId);
    }

    public async Task<IEnumerable<TechEquipment>> GetByClassroomIdAsync(int classroomId) => await GetByClassroomId(classroomId).ToListAsync();

    public async Task<TechEquipment> AddAsync(TechEquipmentModifyDto model)
    {
        if (model.ClassroomId == null && model.Classroom == null)
        {
            throw new ArgumentException("Either ClassroomId or Classroom must be provided");
        }

        if (model.ClassroomId != null && model.Classroom != null && model.ClassroomId != model.Classroom.ClassroomId)
        {
            throw new ArgumentException("ClassroomId and Classroom must be the same");
        }

        var newEquipment = new TechEquipment
        {
            Name = model.Name,
            Description = model.Description,
        };

        if (model.ClassroomId != null)
        {
            newEquipment.ClassroomId = model.ClassroomId.Value;
        }
        else
        {
            newEquipment.Classroom = model.Classroom!;
        }

        var entity = await _context.TechEquipments.AddAsync(newEquipment);
        return entity.Entity;
    }

    public async Task<TechEquipment?> UpdateAsync(int id, TechEquipmentModifyDto model)
    {
        if (model.ClassroomId == null && model.Classroom == null)
        {
            throw new ArgumentException("Either ClassroomId or Classroom must be provided");
        }

        if (model.ClassroomId != null && model.Classroom != null && model.ClassroomId != model.Classroom.ClassroomId)
        {
            throw new ArgumentException("ClassroomId and Classroom must be the same");
        }

        var equipment = await GetByIdAsync(id);
        if (equipment == null)
        {
            return null;
        }

        equipment.Name = model.Name;
        equipment.Description = model.Description;

        if (model.ClassroomId != null)
        {
            equipment.ClassroomId = model.ClassroomId.Value;
        }
        else
        {
            equipment.Classroom = model.Classroom!;
        }

        _context.TechEquipments.Update(equipment);
        return equipment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _context.TechEquipments
            .Where(t => t.EquipmentId == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

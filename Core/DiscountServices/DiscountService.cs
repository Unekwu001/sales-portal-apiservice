using Core.PlanServices;
using Data.Dtos;
using Data.ipNXContext;
using Data.Models.DiscountModel;
using Data.Models.PlanModels;
using Data.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly IpNxDbContext _ipnxDbContext;
        private readonly IPlanService _planService;

        public DiscountService(IpNxDbContext ipnxDbContext, IPlanService planService)
        {
            _ipnxDbContext = ipnxDbContext;
            _planService = planService;
        }

        public async Task<IEnumerable<ViewDiscountsDto>> GetAllDiscountsAsync()
        {
            var discounts = await _ipnxDbContext.Discounts
                 .Include(d => d.PlanTypes)
                 .OrderByDescending(d => d.CreatedOnUtc)
                 .ToListAsync();

            var result = discounts.Select(discount => new ViewDiscountsDto
            {
                Id = discount.Id,
                State = discount.State,
                City = discount.City,
                Street = discount.Street,
                Percentage = discount.Percentage,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                PlanTypeDetails = discount.PlanTypes.Select(pt => new PlanTypeDetailDto
                {
                    PlanTypeName = pt.PlanTypeName,
                    Status = pt.IsActive ? "Active" : "Inactive"
                }).ToList(),
                DiscountStatus = discount.IsActive ? "Active" : "Inactive",
                Date = discount.CreatedOnUtc
            }).ToList();

            return result;
        }


        public async Task DeleteDiscountsAsync(Guid discountId)
        {
            var discount = await _ipnxDbContext.Discounts.FindAsync(discountId);
            if (discount != null)
            {
                _ipnxDbContext.Discounts.Remove(discount);
                await _ipnxDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Discount not found", nameof(discountId));
            }
        }

        public async Task UpdateDiscountsAsync(Guid discountId, UpdateDiscountDto updateDiscountDto, Guid userId)
        {
            var discount = await _ipnxDbContext.Discounts
                .Include(d => d.PlanTypes)
                .FirstOrDefaultAsync(d => d.Id == discountId);

            if (discount != null)
            {
                var startDateUtc = updateDiscountDto.StartDate.HasValue ? DateTime.SpecifyKind(updateDiscountDto.StartDate.Value, DateTimeKind.Utc) : (DateTime?)null;
                var endDateUtc = updateDiscountDto.EndDate.HasValue ? DateTime.SpecifyKind(updateDiscountDto.EndDate.Value, DateTimeKind.Utc) : (DateTime?)null;

                discount.EndDate = endDateUtc;
                discount.StartDate = startDateUtc;
                discount.Street = updateDiscountDto.Street;
                discount.City = updateDiscountDto.City;
                discount.State = updateDiscountDto.State;
                discount.Percentage = updateDiscountDto.Percentage;
                discount.LastUpdatedById = userId;
                discount.LastUpdatedOnUtc = DateTime.UtcNow;

                // Validate and add new PlanTypes to the existing collection
                foreach (var id in updateDiscountDto.PlanTypeIds)
                {
                    var existingPlanType = await _planService.GetPlanTypeByIdAsync(id);
                    if (existingPlanType == null)
                    {
                        throw new ArgumentException($"Plan type with Id {id} was not found.", nameof(updateDiscountDto.PlanTypeIds));
                    }

                    if (!discount.PlanTypes.Any(pt => pt.Id == existingPlanType.Id))
                    {
                        discount.PlanTypes.Add(existingPlanType);
                    }
                }

                await _ipnxDbContext.SaveChangesAsync();
            }
        }


        public async Task ToggleDiscountStatusAsync(Guid discountId, bool activate) 
        {
            var discount = await _ipnxDbContext.Discounts.FindAsync(discountId);
            if (discount != null)
            {
                discount.IsActive = activate;
                await _ipnxDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Discount not found", nameof(discountId));
            }
        }

        public async Task<IEnumerable<ViewDiscountsDto>> GetAllActiveDiscountsAsync()
        {
            var discounts = await _ipnxDbContext.Discounts
                .Where(d => d.IsActive)
                .Include(d => d.PlanTypes)
                .OrderByDescending(d => d.CreatedOnUtc)
                .ToListAsync();

            var result = discounts.Select(d => new ViewDiscountsDto
            {
                Id = d.Id,
                State = d.State,
                City = d.City,
                Street = d.Street,
                Percentage = d.Percentage,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                PlanTypeDetails = d.PlanTypes.Select(pt => new PlanTypeDetailDto
                {
                    PlanTypeName = pt.PlanTypeName,
                    Status = pt.IsActive ? "Active" : "Inactive"
                }).ToList(),
                DiscountStatus = d.IsActive ? "Active" : "Inactive",
                Date = d.CreatedOnUtc
            });

            return result;
        }


        public async Task<bool> StreetNameExistsAsync(string state,string streetName) 
        {
            return await _ipnxDbContext.Discounts.AnyAsync(c => c.State == state && c.Street == streetName);      
        }



        public async Task<Discount> FetchStreetNameDiscountAsync(string state, string streetName)
        {
            return await _ipnxDbContext.Discounts.FirstOrDefaultAsync(c => c.State == state && c.Street == streetName);
        }




        public async Task AddAndSaveDiscountAsync(DiscountDto discountDto, Guid userId, ICollection<PlanType> planTypes)
        {

            var startDateUtc = discountDto.StartDate.HasValue? DateTime.SpecifyKind(discountDto.StartDate.Value, DateTimeKind.Utc) : (DateTime?)null;
            var endDateUtc = discountDto.EndDate.HasValue? DateTime.SpecifyKind(discountDto.EndDate.Value, DateTimeKind.Utc): (DateTime?)null;

            var discount = new Discount
            {

                EndDate = endDateUtc,
                StartDate = startDateUtc,
                City = discountDto.City,
                CreatedById = userId,
                IsActive = false,
                Percentage = discountDto.Percentage,
                State = discountDto.State,  
                Street = discountDto.Street,
                PlanTypes = planTypes
            };
            await _ipnxDbContext.Discounts.AddAsync(discount);
            await _ipnxDbContext.SaveChangesAsync();      
        }





    }
}

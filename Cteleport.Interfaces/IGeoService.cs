using Cteleport.Model;
using System;
using System.Threading.Tasks;

namespace Cteleport.Interfaces
{
    public interface IGeoService
    {
        Task<ResponseResult<DistanceResponse>> CalculateDistance(DistanceRequest request);
    }
}

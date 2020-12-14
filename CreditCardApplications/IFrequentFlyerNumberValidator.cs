using System;

namespace CreditCardApplications
{
    public interface ILicenseData
    {
        string LicenceKey { get;  }
    }

    public interface IServiceInformation
    {
        ILicenseData License { get;  }
    }
    public interface IFrequentFlyerNumberValidator
    {
        bool IsValid(string frequentFlyerNumber);
        void IsValid(string frequentFlyerNumber, out bool isValid);
        IServiceInformation ServiceInformation { get; }
    }
}
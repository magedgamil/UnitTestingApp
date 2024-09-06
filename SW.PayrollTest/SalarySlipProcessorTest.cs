
using Moq;
using SW.Payroll;

namespace SW.PayrollTest;

public class SalarySlipProcessorTest
{
    //[Fact]
    //public void Method_Senario_Outcome()
    //{
    //    //
    //}

    [Fact]
    public void CalculateBasicSalary_ForEmplyoeWageAndWorkingDays_BasicSalary()
    {
        //arange 

        var employee = new Employee
        {
            Wage = 500m,
            WorkingDays = 20
        };

        //act
        var salarySlipProcessor = new SalarySlipProcessor(null);
        var actual = salarySlipProcessor.CalculateBasicSalary(employee);
        var expected = 10000m;
        //assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateBasicSalary_ForEmployeeIsNull_ThrowArgumentNullException()
    {
        // Arrange
        Employee? employee = null;

        // Act
        var salarySlipProcessor = new SalarySlipProcessor(null);

        // Assert
        Assert.Throws<ArgumentNullException>(() => salarySlipProcessor.CalculateBasicSalary(employee));
    }

    [Fact]
    public void CalculateTransportationAllowece_ForEmplyoWorkInOffice_TransportationAllowece()
    {
        //arange 

        var employee = new Employee
        {
            WorkPlatform = WorkPlatform.Office,
        };

        //act
        var salarySlipProcessor = new SalarySlipProcessor(null);
        var actual = salarySlipProcessor.CalculateTransportationAllowece(employee);
        var expected = Constants.TransportationAllowanceAmount;
        //assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateTransportationAllowece_ForEmplyoWorkInRemote_TransportationAllowece()
    {
        //arange 

        var employee = new Employee
        {
            WorkPlatform = WorkPlatform.Remote,
        };

        //act
        var salarySlipProcessor = new SalarySlipProcessor(null);
        var actual = salarySlipProcessor.CalculateTransportationAllowece(employee);
        var expected = 0;
        //assert

        Assert.Equal(expected, actual);
    }
    [Fact]
    public void CalculateTransportationAllowece_ForEmplyoWorkHypird_TransportationAllowece()
    {
        //arange 

        var employee = new Employee
        {
            WorkPlatform = WorkPlatform.Hybrid,
        };

        //act
        var salarySlipProcessor = new SalarySlipProcessor(null);
        var actual = salarySlipProcessor.CalculateTransportationAllowece(employee);
        var expected = Constants.TransportationAllowanceAmount / 2;
        //assert

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateTransportationAllowece_ForEmployeeIsNull_ThrowArgumentNullException()
    {
        // Arrange
        Employee? employee = null;

        // Act
        var salarySlipProcessor = new SalarySlipProcessor(null);

        // Assert
        Assert.Throws<ArgumentNullException>(() => salarySlipProcessor.CalculateTransportationAllowece(employee));
    }

    [Fact]
    public void CalculateDangerPay_ForEmployeeIsNull_ThrowArgumentNullException()
    {
        // Arrange
        Employee? employee = null;

        // Act
        var salarySlipProcessor = new SalarySlipProcessor(null);

        // Assert
        Assert.Throws<ArgumentNullException>(() => salarySlipProcessor.CalculateDangerPay(employee));
    }



    [Fact]
    public void CalculateDangerPay_ForEmplyoeeIsDanger_DangerPay()
    {
        //arange 

        var employee = new Employee
        {
            IsDanger = true
        };

        //act
        var salarySlipProcessor = new SalarySlipProcessor(null);
        var actual = salarySlipProcessor.CalculateDangerPay(employee);
        var expected = Constants.DangerPayAmount;
        //assert

        Assert.Equal(expected, actual);
    }
    [Fact]
    public void CalculateDangerPay_ForEmplyoeeIsDangerZone_DangerPay()
    {
        //arange 

        var employee = new Employee
        {
            IsDanger = false,
            DutyStation = "Ukraine"
        };
        var mock = new Mock<IZoneService>();
        var setup = mock.Setup(z => z.IsDangerZone(employee.DutyStation)).Returns(true);

        //act
        var salarySlipProcessor = new SalarySlipProcessor(mock.Object);
        var actual = salarySlipProcessor.CalculateDangerPay(employee);
        var expected = Constants.DangerPayAmount;
        //assert

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void CalculateDangerPay_ForEmplyoeeIsNotDangerZoneAndDangerOff_RetunoZero()
    {
        //arange 

        var employee = new Employee
        {
            IsDanger = false,
            DutyStation = "Ukraine"
        };
        var mock = new Mock<IZoneService>();
        var setup = mock.Setup(z => z.IsDangerZone(employee.DutyStation)).Returns(false);

        //act
        var salarySlipProcessor = new SalarySlipProcessor(mock.Object);
        var actual = salarySlipProcessor.CalculateDangerPay(employee);
        var expected =0;
        //assert

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void CalculateSpouseAllowance_ForEmployeeIsNull_ThrowArgumentNullException()
    {
        // Arrange
        Employee? employee = null;

        // Act
        var salarySlipProcessor = new SalarySlipProcessor(null);

        // Assert
        Assert.Throws<ArgumentNullException>(() => salarySlipProcessor.CalculateSpouseAllowance(employee));
    }

    [Fact]
    public void CalculateSpouseAllowance_ForEmployeeMarried_SpouseAllowance()
    {
        //arange 

        var employee = new Employee
        {
            IsMarried = true
        };

        //act
        var salarySlipProcessor = new SalarySlipProcessor(null);
        var actual = salarySlipProcessor.CalculateSpouseAllowance(employee);
        var expected = Constants.SpouseAllowanceAmount;
        //assert

        Assert.Equal(expected, actual);
    }


    [Fact]
    public void CalculateSpouseAllowance_ForEmployeeNoMarried_SpouseAllowance()
    {
        //arange 

        var employee = new Employee
        {
            IsMarried = false
        };

        //act
        var salarySlipProcessor = new SalarySlipProcessor(null);
        var actual = salarySlipProcessor.CalculateSpouseAllowance(employee);
        var expected = 0m;
        //assert

        Assert.Equal(expected, actual);
    }



}
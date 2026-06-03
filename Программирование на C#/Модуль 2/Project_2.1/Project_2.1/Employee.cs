namespace Project_2._1;

/// <summary>
/// Класс для хранения данных о каждом работнике
/// </summary>
class Employee
{
    private int _workingYear;
    private string _designation;
    private string _experience;
    private string _employmentStatus;
    private double _salaryInRupees;
    private string _employeeLocation;
    private string _companyLocation;
    private string _companySize;
    private int _remoteWorkingRatio;
    private string _employeeData;

    /// <summary>
    /// Конструктор для инициализации полей 
    /// </summary>
    /// <param name="lineData"></param>
    public Employee(string[] lineData)
    {
        _workingYear = Convert.ToInt32(lineData[1]);
        _designation = lineData[2];
        _experience = lineData[3];
        _employmentStatus = lineData[4];
        _salaryInRupees = Convert.ToDouble(lineData[5]);
        _employeeLocation = lineData[6];
        _companyLocation = lineData[7];
        _companySize = lineData[8];
        _remoteWorkingRatio = Convert.ToInt32(lineData[9]);
        _employeeData = $"{_workingYear} {_designation} {_experience} {_employmentStatus} " +
                        $"{_salaryInRupees} {_employeeLocation} {_companyLocation} {_companySize} {_remoteWorkingRatio}";
    }
    
    /// <summary>
    /// Свойство для вывода _workingYear
    /// </summary>
    public int WorkingYear
    {
        get => _workingYear; 
    }

    /// <summary>
    /// Свойство для вывода _designation
    /// </summary>
    public string Designation
    {
        get => _designation; 
    }

    /// <summary>
    /// Свойство для вывода _experience
    /// </summary>
    public string Experience
    {
        get => _experience; 
    }

    /// <summary>
    /// Свойство для вывода _employmentStatus
    /// </summary>

    public string EmploymentStatus
    {
        get => _employmentStatus; 
    }

    /// <summary>
    /// Свойство для вывода _salaryInRupees
    /// </summary>
    public double SalaryInRupees
    {
        get => _salaryInRupees; 
    }

    /// <summary>
    /// Свойство для вывода _employeeLocation
    /// </summary>
    public string EmployeeLocation
    {
        get => _employeeLocation; 
    }

    /// <summary>
    /// Свойство для вывода _companyLocation
    /// </summary>
    public string CompanyLocation
    {
        get =>  _companyLocation; 
    }
    
    /// <summary>
    /// Свойство для вывода _companySize;
    /// </summary>

    public string CompanySize
    {
        get => _companySize; 
    }

    /// <summary>
    /// Свойство для вывода _remoteWorkingRatio;
    /// </summary>
    public int RemoteWorkingRatio
    {
        get => _remoteWorkingRatio; 
    }

    /// <summary>
    /// Свойство для вывода _employeeData
    /// </summary>
    public string EmployeeData
    {
        get => _employeeData; 
    }
    
}
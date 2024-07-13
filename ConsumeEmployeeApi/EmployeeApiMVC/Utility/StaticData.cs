﻿namespace EmployeeApiMVC.Utility
{
    public class StaticData
    {
        public static string CrudAPIUrl { get; set; }
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
        public static string RoleAdmin = "ADMIN";
        public static string RoleCustomer = "CUSTOMER";
        public static string RoleOperator = "OPERATOR";
        public static string RoleAccount = "ACCOUNT";
        public static string RoleTransport = "TRANSPORT";
        public static string TokenValue = "JwtTokenInitialValues";

    }
}

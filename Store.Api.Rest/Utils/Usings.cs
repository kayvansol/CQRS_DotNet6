﻿global using MediatR;
global using FluentValidation;
global using Newtonsoft.Json;
global using Store.Domain;
global using AutoMapper;
global using System.Diagnostics;
global using System.Text;
global using Serilog;
global using Hangfire;
global using Store.Infra.Sql.LogContext;
global using Store.Domain.Log;
global using Store.Domain.Objects;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Store.Core.Commands;
global using Store.Core.Queries;
global using Store.Domain.DTOs;
global using Store.Domain.DTOs.Category;
global using Store.Domain.DTOs.Customer;
global using Store.Domain.DTOs.Product;
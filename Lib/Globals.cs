global using Lib.Commands;
global using Lib.Queries;

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;

global using Sygenic.CommonLib;

global using MappingTypeToType = System.Collections.Generic.Dictionary<System.Type, System.Type>;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("XCheck")]
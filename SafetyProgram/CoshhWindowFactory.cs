﻿using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;
using SafetyProgram.Ribbons;

namespace SafetyProgram
{
    internal sealed class CoshhWindowFactory<T> : IFactory<IWindow<T>>
        where T : IDocument
    {
        private readonly IConfiguration appConfiguration;
        private readonly IService<T> contentService;
        private readonly ICommandInvoker commandInvoker;
        private readonly T content;

        public CoshhWindowFactory(
            IConfiguration appConfiguration,
            IService<T> contentService,
            ICommandInvoker commandInvoker,
            T content
            )
        {
            if (
                appConfiguration != null &&
                contentService != null &&
                commandInvoker != null
                )
            {
                this.appConfiguration = appConfiguration;
                this.commandInvoker = commandInvoker;
                this.contentService = contentService;
                this.content = content;
            }            
        }

        public static IWindow<T> StaticCreateNew(
            IConfiguration appConfiguration,
            IService<T> contentService,
            ICommandInvoker commandInvoker,
            T content
            )
        {
            //Configuration isn't used but will be required eventually when window customization occurs

            return new CoshhWindow<T>(
                contentService,
                content,
                (window) => new CoshhWindowView(window),
                (window) => new WindowICommands<T>(window, commandInvoker),
                (window) => new CoshhRibbon<T>(
                    window,
                    (ribbon) => new CoshhRibbonView(ribbon)
                    )
                );
        }

        public IWindow<T> CreateNew()
        {
            return StaticCreateNew(appConfiguration, contentService, commandInvoker, content);
        }
    }
}

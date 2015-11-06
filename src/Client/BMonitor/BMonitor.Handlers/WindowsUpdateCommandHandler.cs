using System;
using Blob.Contracts.Commands;
using log4net;
using WUApiLib;

namespace BMonitor.Handlers
{
    // http://www.nullskull.com/a/1592/install-windows-updates-using-c--wuapi.aspx
    public class WindowsUpdateCommandHandler : IDeviceCommandHandler<WindowsUpdateCommand>
    {
        private ILog _log;

        public WindowsUpdateCommandHandler(ILog log)
        {
            _log = log;
        }

        public void Handle(WindowsUpdateCommand command)
        {
            _log.Debug("Handling Windows Update Command");
            // Find updates for this computer
            IUpdateSession3 uSession = new UpdateSession();
            IUpdateSearcher uSearcher = uSession.CreateUpdateSearcher();
            ISearchResult uResult = uSearcher.Search("IsInstalled=0 and IsHidden=0 and Type='Software'");
            //ISearchResult uResult = uSearcher.Search("IsInstalled=0 and Type='Software'");

            // List results
            _log.Debug(string.Format("Found {0} updates:", uResult.Updates.Count));
            foreach (IUpdate update in uResult.Updates)
            {
                _log.Debug(string.Format(update.Title));
            }

            // Optionally, remove unwanted updates from the list (or create a new list) before downloading
            //_log.Debug(string.Format("Getting one"));
            _log.Debug(string.Format("Getting updates"));
            //int testOnlyGetOne = 0;
            UpdateCollection updatesToDownload = new UpdateCollection();
            foreach (IUpdate update in uResult.Updates)
            {
                //if (testOnlyGetOne++ == 0)
                    updatesToDownload.Add(update);
            }

            // Download needed updates
            _log.Debug(string.Format("Downloading: {0}", updatesToDownload));
            UpdateDownloader downloader = uSession.CreateUpdateDownloader();
            downloader.Updates = updatesToDownload;
            //downloader.Updates = uResult.Updates;
            downloader.Download();

            // check if downloaded
            _log.Debug(string.Format("checking if downloaded"));
            UpdateCollection updatesToInstall = new UpdateCollection();
            foreach (IUpdate update in updatesToDownload)
            {
                if (update.IsDownloaded)
                    updatesToInstall.Add(update);
            }

            // create installer
            _log.Debug(string.Format("create installer"));
            IUpdateInstaller installer = uSession.CreateUpdateInstaller();
            installer.Updates = updatesToInstall;

            // Go!
            IInstallationResult installationRes = installer.Install();

            // 0x80240024 = 
            // Show results
            for (int i = 0; i < updatesToInstall.Count; i++)
            {
                if (installationRes.GetUpdateResult(i).HResult == 0)
                {
                    _log.Debug(string.Format("Installed : " + updatesToInstall[i].Title));
                }
                else
                {
                    _log.Debug(string.Format("Failed : " + updatesToInstall[i].Title));
                }
            }
        }
        /**
         * https://support.microsoft.com/en-us/kb/938205
         * 
         * success codes
         * 0x240001 WU_S_SERVICE_STOP WindowsUpdate Windows Update Agent was stopped successfully
0x00240002 WU_S_SELFUPDATE Windows Update Agent updated itself
0x00240003 WU_S_UPDATE_ERROR Operation completed successfully but there were errors applying the updates
0x00240004 WU_S_MARKED_FOR_DISCONNECT A callback was marked to be disconnected later because the request to disconnect the operation came while a callback was executing
0x00240005 WU_S_REBOOT_REQUIRED The system must be restarted to complete installation of the update
0x00240006 WU_S_ALREADY_INSTALLED The update to be installed is already installed on the system
0x00240007 WU_S_ALREADY_UNINSTALLED The update to be removed is not installed on the system
0x00240008 WU_S_ALREADY_DOWNLOADED The update to be downloaded has already been downloaded
         * 
         * error codes
         * 0x80240001 WU_E_NO_SERVICE Windows Update Agent was unable to provide the service.
0x80240002 WU_E_MAX_CAPACITY_REACHED The maximum capacity of the service was exceeded.
0x80240003 WU_E_UNKNOWN_ID An ID cannot be found.
0x80240004 WU_E_NOT_INITIALIZED The object could not be initialized.
0x80240005 WU_E_RANGEOVERLAP The update handler requested a byte range overlapping a previously requested range.
0x80240006 WU_E_TOOMANYRANGES The requested number of byte ranges exceeds the maximum number (2^31 - 1).
0x80240007 WU_E_INVALIDINDEX The index to a collection was invalid.
0x80240008 WU_E_ITEMNOTFOUND The key for the item queried could not be found.
0x80240009 WU_E_OPERATIONINPROGRESS Another conflicting operation was in progress. Some operations such as installation cannot be performed twice simultaneously.
0x8024000A WU_E_COULDNOTCANCEL Cancellation of the operation was not allowed.
0x8024000B WU_E_CALL_CANCELLED Operation was cancelled.
0x8024000C WU_E_NOOP No operation was required.
0x8024000D WU_E_XML_MISSINGDATA Windows Update Agent could not find required information in the update's XML data.
0x8024000E WU_E_XML_INVALID Windows Update Agent found invalid information in the update's XML data.
0x8024000F WU_E_CYCLE_DETECTED Circular update relationships were detected in the metadata.
0x80240010 WU_E_TOO_DEEP_RELATION Update relationships too deep to evaluate were evaluated.
0x80240011 WU_E_INVALID_RELATIONSHIP An invalid update relationship was detected.
0x80240012 WU_E_REG_VALUE_INVALID An invalid registry value was read.
0x80240013 WU_E_DUPLICATE_ITEM Operation tried to add a duplicate item to a list.
0x80240016 WU_E_INSTALL_NOT_ALLOWED Operation tried to install while another installation was in progress or the system was pending a mandatory restart.
0x80240017 WU_E_NOT_APPLICABLE Operation was not performed because there are no applicable updates.
0x80240018 WU_E_NO_USERTOKEN Operation failed because a required user token is missing.
0x80240019 WU_E_EXCLUSIVE_INSTALL_CONFLICT An exclusive update cannot be installed with other updates at the same time.
0x8024001A WU_E_POLICY_NOT_SET A policy value was not set.
0x8024001B WU_E_SELFUPDATE_IN_PROGRESS The operation could not be performed because the Windows Update Agent is self-updating.
0x8024001D WU_E_INVALID_UPDATE An update contains invalid metadata.
0x8024001E WU_E_SERVICE_STOP Operation did not complete because the service or system was being shut down.
0x8024001F WU_E_NO_CONNECTION Operation did not complete because the network connection was unavailable.
0x80240020 WU_E_NO_INTERACTIVE_USER Operation did not complete because there is no logged-on interactive user.
0x80240021 WU_E_TIME_OUT Operation did not complete because it timed out.
0x80240022 WU_E_ALL_UPDATES_FAILED Operation failed for all the updates.
0x80240023 WU_E_EULAS_DECLINED The license terms for all updates were declined.
0x80240024 WU_E_NO_UPDATE There are no updates.
0x80240025 WU_E_USER_ACCESS_DISABLED Group Policy settings prevented access to Windows Update.
0x80240026 WU_E_INVALID_UPDATE_TYPE The type of update is invalid.
0x80240027 WU_E_URL_TOO_LONG The URL exceeded the maximum length.
0x80240028 WU_E_UNINSTALL_NOT_ALLOWED The update could not be uninstalled because the request did not originate from a WSUS server.
0x80240029 WU_E_INVALID_PRODUCT_LICENSE Search may have missed some updates before there is an unlicensed application on the system.
0x8024002A WU_E_MISSING_HANDLER A component required to detect applicable updates was missing.
0x8024002B WU_E_LEGACYSERVER An operation did not complete because it requires a newer version of server.
0x8024002C WU_E_BIN_SOURCE_ABSENT A delta-compressed update could not be installed because it required the source.
0x8024002D WU_E_SOURCE_ABSENT A full-file update could not be installed because it required the source.
0x8024002E WU_E_WU_DISABLED Access to an unmanaged server is not allowed.
0x8024002F WU_E_CALL_CANCELLED_BY_POLICY Operation did not complete because the DisableWindowsUpdateAccess policy was set.
0x80240030 WU_E_INVALID_PROXY_SERVER The format of the proxy list was invalid.
0x80240031 WU_E_INVALID_FILE The file is in the wrong format.
0x80240032 WU_E_INVALID_CRITERIA The search criteria string was invalid.
0x80240033 WU_E_EULA_UNAVAILABLE License terms could not be downloaded.
0x80240034 WU_E_DOWNLOAD_FAILED Update failed to download.
0x80240035 WU_E_UPDATE_NOT_PROCESSED The update was not processed.
0x80240036 WU_E_INVALID_OPERATION The object's current state did not allow the operation.
0x80240037 WU_E_NOT_SUPPORTED The functionality for the operation is not supported.
0x80240038 WU_E_WINHTTP_INVALID_FILE The downloaded file has an unexpected content type.
0x80240039 WU_E_TOO_MANY_RESYNC Agent is asked by server to resync too many times.
0x80240040 WU_E_NO_SERVER_CORE_SUPPORT WUA API method does not run on Server Core installation.
0x80240041 WU_E_SYSPREP_IN_PROGRESS Service is not available while sysprep is running.
0x80240042 WU_E_UNKNOWN_SERVICE The update service is no longer registered with AU.
0x80240FFF WU_E_UNEXPECTED An operation failed due to reasons not covered by another error code.
         * 
         * 
         * minor errors
         * 0x80241001 WU_E_MSI_WRONG_VERSION Search may have missed some updates because the Windows Installer is less than version 3.1.
0x80241002 WU_E_MSI_NOT_CONFIGURED Search may have missed some updates because the Windows Installer is not configured.
0x80241003 WU_E_MSP_DISABLED Search may have missed some updates because policy has disabled Windows Installer patching.
0x80241004 WU_E_MSI_WRONG_APP_CONTEXT An update could not be applied because the application is installed per-user.
0x80241FFF WU_E_MSP_UNEXPECTED Search may have missed some updates because there was a failure of the Windows Installer.
         * */
    }
}
